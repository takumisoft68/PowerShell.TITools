using System;
using System.Management.Automation;

namespace TITools
{
    [Cmdlet( VerbsDiagnostic.Resolve , "MyCmdlet")]
    public class ResolveMyCmdletCommand : PSCmdlet
    {
        [Parameter(Position=0)]
        public Object InputObject { get; set; }

        [Parameter(Position=1)]
        public Object Input2Object { get; set; }

        protected override void EndProcessing()
        {
            this.WriteObject($"{this.InputObject}, {this.Input2Object}");
            base.EndProcessing();
        }
    }

    [Cmdlet( VerbsCommon.Get , "MyCmdlet")]
    public class GetMyCmdletCommand : PSCmdlet
    {
        [Parameter(Position=0)]
        public Object InputObject { get; set; }

        [Parameter(Position=1)]
        public Object Input2Object { get; set; }

        protected override void EndProcessing()
        {
            this.WriteObject($"{this.InputObject}, {this.Input2Object}");
            base.EndProcessing();
        }
    }
}
