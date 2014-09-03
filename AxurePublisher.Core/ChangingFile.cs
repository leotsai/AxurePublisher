using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AxurePublisher.Core
{
    public class ChangingFile
    {
        public string ServerPath { get; set; }
        public string FullPath { get; set; }
        public WatcherChangeTypes ChangeType { get; set; }
        public string Name { get; set; }

        public ChangingFile(FileSystemEventArgs e)
        {
            this.ServerPath = e.FullPath.Replace(AppContext.WatchingFolder, "").Replace("\\,", "/");
            this.FullPath = e.FullPath;
            this.ChangeType = e.ChangeType;
            this.Name = e.Name;
        }
    }
}
