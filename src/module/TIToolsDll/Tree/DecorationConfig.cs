using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TIToolsDll.Tree
{
    public class DecorationConfig
    {
        // 接頭語(マージン)
        public string PreMargin { set; get; } = "│";
        public string PreMarginLast { set; get; } = "  ";

        // 接頭語(ディレクトリ)
        public string PreDir { set; get; } = "├─ ";
        public string PreDirLast { set; get; } = "└─ ";

        // 接頭語(ファイル)
        public string PreFile { set; get; } = "    ";
        public string PreFileLast { set; get; } = "    ";

        // 接頭語(ファイル, サブディレクトリが同階層にある場合)
        public string PreFileWithDir { set; get; } = "│  ";

        // インデント
        public string Indent { set; get; } = "   ";
        public int BeforeFileMargin { set; get; } = 0;
        public int BeforeDirMargin { set; get; } = 1;

        public DecorationConfig() { }
    }
}
