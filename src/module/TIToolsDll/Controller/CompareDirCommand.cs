using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using TIToolsDll.Compare;
using TIToolsDll.Utility;
using TIToolsDll.Controller.Threading;


namespace TIToolsDll.Controller
{
    [Cmdlet( VerbsData.Compare , "Dir")]
    public class CompareDirCommand : AwaitablePSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = false,
            ValueFromPipelineByPropertyName = true)]
        public string DirPathA { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipeline = false,
            ValueFromPipelineByPropertyName = true)]
        public string DirPathB { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 2,
            ValueFromPipelineByPropertyName = true)]
        public DiffMethod Method { get; set; } = DiffMethod.ExistOrNot;

        [Parameter(
            Mandatory = false,
            Position = 3,
            ValueFromPipelineByPropertyName = true)]
        public SwitchParameter Full { get; set; } = false;

        [Parameter(
            Mandatory = false,
            Position = 4,
            ValueFromPipelineByPropertyName = true)]
        public int ThreadCount { get; set; } = 2;



        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("BeginProcessing");
            base.BeginProcessing();
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override async Task ProcessRecordAsync()
        {
            InvokeWriteLine("Dir Path [A] : " + DirPathA);
            InvokeWriteLine("Dir Path [B] : " + DirPathB);

            await Task.Delay(1);

            var compare = new DirCompare(new LogHandler(
                logProgress: log => InvokeWriteLine(log),
                logVerbose: log => InvokeWriteVerbose(log),
                logDebug: log => InvokeWriteDebug(log),
                logWarning: log => InvokeWriteWarning(log),
                logError: exp => InvokeThrowTerminatingError(exp)
            ));

            var diffs = await compare.GetDiff(DirPathA, DirPathB, Method, ThreadCount);
            InvokeWriteLine("Diff files : " + diffs.Count());

            var unmatched = diffs.Where(v => v.Reason != DiffReason.Match);
            InvokeWriteLine("Unmatched  : " + unmatched.Count());

            if (Full)
            {
                foreach(var diff in diffs)
                    InvokeWriteObject(diff);
            }
            else
            {
                foreach (var diff in unmatched)
                    InvokeWriteObject(diff);
            }
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            base.EndProcessing();
        }
    }
}
