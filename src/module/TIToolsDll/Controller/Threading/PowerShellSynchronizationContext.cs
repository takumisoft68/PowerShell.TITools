using System;
using System.Collections.Concurrent;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;


namespace TIToolsDll.Controller.Threading
{
    public class PowerShellSynchronizationContext : IDisposable
    {
        private readonly BlockingCollection<Action> _queue = new BlockingCollection<Action>();

        public bool IsCompleted => _queue.IsCompleted;

        public void Post(Action action)
        {
            _queue.Add(action);
        }

        public void RunLoop(CancellationToken? ct)
        {
            if (ct?.IsCancellationRequested == true)
                throw new ArgumentException("CancellationToken がキャンセル済みです");

            // CancellationToken にループ終了処理を紐づける
            ct?.Register(() => Terminate());

            // _queue を処理するループ
            // Terminate() が呼ばれ、かつ _queue が空になると
            // TryTake が false になってループを抜ける
            while (_queue.TryTake(out var action, Timeout.Infinite))
            {
                action?.Invoke();
            }
        }

        /// <summary>
        /// Loopを止める
        /// </summary>
        public void Terminate()
        {
            if (_queue.IsCompleted)
                return;

            _queue.CompleteAdding();
            while(!_queue.IsCompleted)
            {
                Thread.Sleep(10);
            }
        }

        public void Dispose() => Terminate();
    }
}