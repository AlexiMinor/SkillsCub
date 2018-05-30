using System;
using System.IO;

namespace SkillsCub.Core
{
    public class AttachedFile
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public Uri Link { get; set; }
        public Stream FileStream { get; set; }
    }
}