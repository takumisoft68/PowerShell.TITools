using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using TIToolsDll.Tree;

namespace TIToolsDll.Controller
{
    [Cmdlet( VerbsCommon.Get , "Tree")]
    public class GetTreeCommand : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string DirPath { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        public DecorationType Deco { get; set; } = DecorationType.Default;

        [Parameter(
            Mandatory = false,
            Position = 2,
            ValueFromPipelineByPropertyName = true)]
        public bool DirOnly { get; set; } = false;

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            WriteVerbose("Process!" + DirPath);

            var config = DecorationConfigFactory.GetConfig(Deco);
            var diag = Tree.Tree.Make(DirPath, DirOnly, config);

            WriteObject(diag);
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            WriteVerbose("End!");
            base.EndProcessing();
        }

    }
}
