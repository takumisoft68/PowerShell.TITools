namespace TIToolsDll.Compare
{
    public class DiffInfo
    {
        public DiffReason Reason { get; set; } = DiffReason.Match;
        public string RelatedPath { get; set; } = null;
        public string PathA { get; set; } = null;
        public string PathB { get; set; } = null;
    }
}
