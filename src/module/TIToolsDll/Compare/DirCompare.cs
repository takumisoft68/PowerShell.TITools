using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using TIToolsDll.Utility;

namespace TIToolsDll.Compare
{
    public class DirCompare
    {
        public DirCompare(ILogHandler logger)
        {
            _logger = logger;
        }

        private ILogHandler _logger { get; } = null;

        public async Task<DiffInfo[]> GetDiff(string rootPathA, string rootPathB, DiffMethod method, int threadCount)
        {
            await Task.Delay(1);

            _logger?.LogVerbose($"Begin diff.");
            if (threadCount < 1)
                throw new ArgumentOutOfRangeException("threadCount", "1以上を指定する必要があります");

            var dirNodeA = DirNode.MakeDirNode(rootPathA);
            var dirNodeB = DirNode.MakeDirNode(rootPathB);

            var fileFullPathsA = LineFiles(dirNodeA);
            var fileFullPathsB = LineFiles(dirNodeB);

            var fileRelatedPathsA = fileFullPathsA.Select(v => v.Replace(rootPathA, "").TrimStart('\\'));
            var fileRelatedPathsB = fileFullPathsB.Select(v => v.Replace(rootPathB, "").TrimStart('\\'));

            // いずれかのフォルダに含まれるファイルの総リスト
            var files = fileRelatedPathsA.ToList();
            files.AddRange(fileRelatedPathsB.Where(v => !fileRelatedPathsA.Contains(v)).ToList());

            // 比較リストを作成
            var diffList = new DiffInfo[files.Count];
            foreach (var x in files.Select((file, index) => new { file, index }))
            {
                diffList[x.index] = new DiffInfo();
                diffList[x.index].RelatedPath = x.file;
                diffList[x.index].PathA = fileFullPathsA.Contains(rootPathA + "\\" + x.file)
                    ? rootPathA + "\\" + x.file
                    : null;
                diffList[x.index].PathB = fileFullPathsB.Contains(rootPathB + "\\" + x.file)
                    ? rootPathB + "\\" + x.file
                    : null;
                diffList[x.index].Reason = DiffReason.Match;
            }



            // 存在比較
            if (method == DiffMethod.ExistOrNot || method == DiffMethod.ExistOrNot_Timestamp || method == DiffMethod.ExistOrNot_MD5Hash)
            {
                _logger?.LogVerbose($"Begin ExistOrNot compare.");
                foreach (var diffItem in diffList)
                {
                    // 差分ありと判定済みのファイルは無視
                    if (diffItem.Reason != DiffReason.Match)
                        continue;

                    if (diffItem.PathA != null && diffItem.PathB != null)
                        diffItem.Reason = DiffReason.Match;
                    else if (diffItem.PathA != null && diffItem.PathB == null)
                        diffItem.Reason = DiffReason.OnlyInA;
                    else if (diffItem.PathA == null && diffItem.PathB != null)
                        diffItem.Reason = DiffReason.OnlyInB;
                    else
                        throw new NotSupportedException("処理例外。ファイルパスが正しく処理できないファイルが含まれています。");
                }
                _logger?.LogVerbose($"Complete ExistOrNot compare.");
            }

            // タイムスタンプ比較
            if (method == DiffMethod.ExistOrNot_Timestamp)
            {
                _logger?.LogVerbose($"Begin Timestamp compare.");
                // 差分ありと判定済みのファイルは無視
                var queue = diffList.Where(v => v.Reason == DiffReason.Match);
                _logger?.LogVerbose($"Queue count of compare Timestamp : {queue.Count()}");

                foreach (var diffItem in queue)
                {
                    if (diffItem.PathA == null && diffItem.PathB == null)
                        throw new NotSupportedException("処理例外。ファイルパスが正しく処理できないファイルが含まれています。");

                    var timestampA = File.GetLastWriteTime(diffItem.PathA);
                    var timestampB = File.GetLastWriteTime(diffItem.PathB);
                    if (timestampA != timestampB)
                    {
                        _logger?.LogVerbose($"Completing Timestamp {diffItem.RelatedPath}. Not match.");
                        diffItem.Reason = DiffReason.TimestampNotMatch;
                    }
                    else
                    {
                        _logger?.LogVerbose($"Completing Timestamp {diffItem.RelatedPath}. Match.");
                    }
                }
                _logger?.LogVerbose($"Complete Timestamp compare.");
            }

            // MD5ハッシュ比較
            if (method == DiffMethod.ExistOrNot_MD5Hash)
            {
                _logger?.LogVerbose($"Begin MD5Hash compare.");
                var queue = new BlockingCollection<DiffInfo>();
                foreach (var diffItem in diffList.Where(v => v.Reason == DiffReason.Match))
                {
                    // 差分ありと判定済みのファイルは無視
                    queue.Add(diffItem);
                }
                queue.CompleteAdding();
                _logger?.LogVerbose($"Queue count of compare MD5Hash : {queue.Count}");

                if (queue.Count >= 0)
                {
                    var taskList = new List<Task>();

                    foreach (var index in Enumerable.Range(1, threadCount))
                    {
                        taskList.Add(CompareMD5HashAsync(index, queue, _logger));
                    }
                    Task.WaitAll(taskList.ToArray());
                }

                _logger?.LogVerbose($"Complete MD5Hash compare.");
            }

            await Task.Delay(1);
            return diffList;
        }

        /// <summary>
        /// DirNode(階層構造)のファイルを一列のList<string>にする
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string[] LineFiles(DirNode node)
        {
            var lines = new List<string>();
            lines.AddRange(node.Files);
            foreach (var dir in node.ChildrenDir)
            {
                lines.AddRange(LineFiles(dir));
            }
            return lines.ToArray();
        }

        private async Task CompareMD5HashAsync(int taskIndex, BlockingCollection<DiffInfo> queue, ILogHandler logger)
        {
            await Task.Delay(1);
            logger?.LogVerbose($"[thread {taskIndex}] Start compare thread...");

            using (var md5 = new MD5CryptoServiceProvider())
            {
                while (true)
                {
                    var diffItem = null as DiffInfo;
                    if (!queue.TryTake(out diffItem, 1000))
                    {
                        logger?.LogVerbose($"[thread {taskIndex}] Queue is empty.");
                        break;
                    }

                    logger?.LogVerbose($"[thread {taskIndex}] Comparing ({diffItem.RelatedPath})...");

                    if (diffItem.PathA == null && diffItem.PathB == null)
                        throw new NotSupportedException("処理例外。ファイルパスが正しく処理できないファイルが含まれています。");

                    using (var fsA = new FileStream(diffItem.PathA, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (var fsB = new FileStream(diffItem.PathB, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var hashByteA = md5.ComputeHash(fsA);
                        var hashByteB = md5.ComputeHash(fsB);
                        var hashA = BitConverter.ToString(hashByteA).ToLower().Replace("-", "");
                        var hashB = BitConverter.ToString(hashByteB).ToLower().Replace("-", "");
                        if (hashA != hashB)
                        {
                            diffItem.Reason = DiffReason.MD5HashNotMatch;
                        }
                        _logger?.LogVerbose($"[thread {taskIndex}] Completed comparing ({diffItem.RelatedPath})... {hashA == hashB}");
                    }
                }
            }

            await Task.Delay(1);
            logger?.LogVerbose($"[thread {taskIndex}] Complete compare thread...");
        }
    }
}
