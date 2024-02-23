using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TIToolsDll.Tree
{
    public class DecorationConfigFactory
    {
        static DecorationConfig DecorationDefault = new DecorationConfig(){
            BeforeDirMargin = 1,
            BeforeFileMargin = 0,
            Indent = "   ",
            PreDir = "├─ ",
            PreDirLast = "└─ ",
            PreFile = "    ",
            PreFileLast = "    ",
            PreFileWithDir = "│  ",
            PreMargin = "│",
            PreMarginLast = "  "
        };

        static DecorationConfig DecorationDotLine = new DecorationConfig(){
            BeforeDirMargin = 1,
            BeforeFileMargin = 0,
            Indent = "   ",
            PreDir = "├─ ",
            PreDirLast = "└─ ",
            PreFile = "│… ",
            PreFileLast = "│… ",
            PreFileWithDir = "│… ",
            PreMargin = "│",
            PreMarginLast = "  "
        };

        static DecorationConfig DecorationShort = new DecorationConfig(){
            BeforeDirMargin = 0,
            BeforeFileMargin = 0,
            Indent = "  ",
            PreDir = "├ ",
            PreDirLast = "└ ",
            PreFile = "   ",
            PreFileLast = "   ",
            PreFileWithDir = "│ ",
            PreMargin = "│",
            PreMarginLast = "  "
        };

        static DecorationConfig DecorationAscii = new DecorationConfig(){
            BeforeDirMargin = 1,
            BeforeFileMargin = 0,
            Indent = "   ",
            PreDir = " +-- ",
            PreDirLast = " +-- ",
            PreFile = "     ",
            PreFileLast = "     ",
            PreFileWithDir = " |   ",
            PreMargin = " |",
            PreMarginLast = "  "
        };

        static DecorationConfig DecorationAsciiWide = new DecorationConfig(){
            BeforeDirMargin = 1,
            BeforeFileMargin = 0,
            Indent = "      ",
            PreDir = " +-- ",
            PreDirLast = " +-- ",
            PreFile = "     ",
            PreFileLast = "     ",
            PreFileWithDir = " |   ",
            PreMargin = " |",
            PreMarginLast = "  "
        };

        static DecorationConfig DecorationAsciiShort = new DecorationConfig(){
            BeforeDirMargin = 0,
            BeforeFileMargin = 0,
            Indent = "  ",
            PreDir = " +- ",
            PreDirLast = " +- ",
            PreFile = "    ",
            PreFileLast = "    ",
            PreFileWithDir = " |  ",
            PreMargin = " |",
            PreMarginLast = "  "
        };

        static DecorationConfig DecorationAsciiVeryShort = new DecorationConfig(){
            BeforeDirMargin = 0,
            BeforeFileMargin = 0,
            Indent = "  ",
            PreDir = "+- ",
            PreDirLast = "+- ",
            PreFile = "   ",
            PreFileLast = "   ",
            PreFileWithDir = "|  ",
            PreMargin = "|",
            PreMarginLast = " "
        };

        static DecorationConfig DecorationAsciiUltraShort = new DecorationConfig(){
            BeforeDirMargin = 0,
            BeforeFileMargin = 0,
            Indent = "  ",
            PreDir = "+-",
            PreDirLast = "+-",
            PreFile = "  ",
            PreFileLast = "  ",
            PreFileWithDir = "| ",
            PreMargin = "|",
            PreMarginLast = " "
        };


        public static DecorationConfig GetConfig(DecorationType type)
        {
            switch (type)
            {
                case DecorationType.Default: return DecorationDefault;
                case DecorationType.DotLine: return DecorationDotLine;
                case DecorationType.Short: return DecorationShort;
                case DecorationType.Ascii: return DecorationAscii;
                case DecorationType.AsciiWide: return DecorationAsciiWide;
                case DecorationType.AsciiShort: return DecorationAsciiShort;
                case DecorationType.AsciiVeryShort: return DecorationAsciiVeryShort;
                case DecorationType.AsciiUltraShort: return DecorationAsciiUltraShort;
                default: return DecorationDefault;
            }
        }
    }
}
