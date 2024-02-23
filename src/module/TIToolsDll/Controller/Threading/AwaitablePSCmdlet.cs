using System;
using System.Collections.Concurrent;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;


namespace TIToolsDll.Controller.Threading
{
    public class AwaitablePSCmdlet : PSCmdlet
    {
        protected PowerShellSynchronizationContext _PSSyncCtx = null;

        protected override sealed void ProcessRecord()
        {
            using(_PSSyncCtx = new PowerShellSynchronizationContext())
            using(var cst = new CancellationTokenSource())
            {
                // ProcessRecordAsyncタスクを開始する
                var task = ProcessRecordAsync();
                if(task == null) { return; }

                // ProcessRecordAsyncタスクの完了時に
                // _PSSyncCtxのループも終了させる
                task.ContinueWith(_ => cst.Cancel(), TaskScheduler.Default);

                // ProcessRecord() が呼ばれたスレッド上で
                // 実行させたい処理を代理実行するループを開始する
                _PSSyncCtx.RunLoop(cst.Token);

                // 正常にRunLoopを抜けてきたなら
                // task はそれより前に完了しているはずだが、
                // 異常系も想定して完了を待機する
                task.GetAwaiter().GetResult();
            }
            _PSSyncCtx = null;
        }

        protected virtual Task ProcessRecordAsync()
        {
            throw new NotImplementedException();
        }

        protected void Invoke(Action action)
        {
            // _PSSyncCtx に ProcessRecord() が呼ばれたスレッド上で実行してもらう
            _PSSyncCtx?.Post(action);
        }

        protected void InvokeWriteObject(object sendToPipeline)
            => Invoke(() => WriteObject(sendToPipeline));

        protected void InvokeWriteLine(string message)
            => Invoke(() => Console.WriteLine(message));

        protected void InvokeWriteVerbose(string message)
            => Invoke(() => WriteVerbose(message));

        protected void InvokeWriteDebug(string message)
            => Invoke(() => WriteDebug(message));

        protected void InvokeWriteWarning(string message)
            => Invoke(() => WriteWarning(message));

        protected void InvokeWriteError(Exception exception)
        {
            if (exception is IContainsErrorRecord err
                && err.ErrorRecord != null)
            {
                WriteError(err.ErrorRecord);
            }
            else
            {
                WriteError(
                    new ErrorRecord(exception, exception.Message,
                        ErrorCategory.InvalidResult, null)
                );
            }
        }

        protected void InvokeThrowTerminatingError(Exception exception)
        {
            if (exception is IContainsErrorRecord err
                && err.ErrorRecord != null)
            {
                ThrowTerminatingError(err.ErrorRecord);
            }
            else
            {
                ThrowTerminatingError(
                    new ErrorRecord(exception, exception.Message,
                        ErrorCategory.InvalidResult, null)
                );
            }
        }
    }
}