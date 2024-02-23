using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using TIToolsDll.Utility;

namespace TIToolsDll.Tree
{
    class Tree
    {
        public static string Make(string rootPath, bool dirOnly, DecorationConfig config)
        {
            var sb = new StringBuilder();

            void make(DirNode _dirNode, string parentIndent)
            {
                void BeforeFileMargin()
                {
                    // 指定行開ける
                    for (var i = 0; i < config.BeforeFileMargin; i++)
                    {
                        sb.Append(parentIndent + config.PreMargin + Environment.NewLine);
                    }
                }

                void BeforeDirMargin()
                {
                    // 指定行開ける
                    for (var i = 0; i < config.BeforeDirMargin; i++)
                    {
                        sb.Append(parentIndent + config.PreMargin + Environment.NewLine);
                    }
                }

                var dirs = _dirNode.ChildrenDir;
                var files = dirOnly ? new string[] { } : _dirNode.Files;

                BeforeFileMargin();

                if (dirs.Length == 0)
                {
                    // ファイル
                    files.Take(files.Length - 1).ToList().ForEach(file =>
                    {
                        var name = System.IO.Path.GetFileName(file);
                        sb.Append(parentIndent + config.PreFile + name + Environment.NewLine);
                    });
                    // ファイル（最後）
                    files.Skip(files.Length - 1).ToList().ForEach(file =>
                    {
                        var name = System.IO.Path.GetFileName(file);
                        sb.Append(parentIndent + config.PreFileLast + name + Environment.NewLine);
                    });
                    return;
                }
                else
                {
                    // ファイル（同階層にディレクトリがある場合）
                    files.ToList().ForEach(file =>
                    {
                        var name = System.IO.Path.GetFileName(file);
                        sb.Append(parentIndent + config.PreFileWithDir + name + Environment.NewLine);
                    });
                }


                // サブディレクトリ
                dirs.Take(dirs.Length - 1).ToList().ForEach(dir =>
                {
                    BeforeDirMargin();
                    var name = System.IO.Path.GetFileName(dir.Path);
                    sb.Append(parentIndent + config.PreDir + name + Environment.NewLine);
                    make(dir, parentIndent + config.PreMargin + config.Indent);
                });
                // サブディレクトリ最後
                dirs.Skip(dirs.Length - 1).ToList().ForEach(dir =>
                {
                    BeforeDirMargin();
                    var name = System.IO.Path.GetFileName(dir.Path);
                    sb.Append(parentIndent + config.PreDirLast + name + Environment.NewLine);
                    make(dir, parentIndent + config.PreMarginLast + config.Indent);
                });
            }

            if (config == null)
                throw new ArgumentNullException("config can not be null.");

            var dirNode = DirNode.MakeDirNode(rootPath);
            sb.Append(rootPath + Environment.NewLine);
            make(dirNode, " ");
            return sb.ToString();
        }

    }
}
