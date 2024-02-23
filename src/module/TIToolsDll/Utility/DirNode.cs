using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace TIToolsDll.Utility
{
    public class DirNode
    {
        public DirNode(string path) { Path = path; }
        public string Path { get; }
        public string[] Files { get; set; }
        public DirNode[] ChildrenDir { get; set; }

        public static DirNode MakeDirNode(string path)
        {
            var childrenNode = new List<DirNode>();
            var childrenDirPaths = Directory.GetDirectories(path);
            foreach (var childDirPath in childrenDirPaths)
            {
                var childDir = MakeDirNode(childDirPath);
                childrenNode.Add(childDir);
            }

            var dir = new DirNode(path);
            dir.Files = Directory.GetFiles(path, "*").OrderBy(f => f).ToArray();
            dir.ChildrenDir = childrenNode.OrderBy(f => f.Path).ToArray();
            return dir;
        }
    }
}
