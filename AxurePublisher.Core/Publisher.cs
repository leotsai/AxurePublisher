using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AxurePublisher.Core
{
    public class Publisher
    {
        private static FileSystemWatcher _watcher;

        public static void Start()
        {
            _watcher = new FileSystemWatcher(AppContext.WatchingFolder);
            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;
            _watcher.Changed += FolderChanged;
        }

        private static void FolderChanged(object sender, FileSystemEventArgs e)
        {
            UploaderQueue.AddFile(new ChangingFile(e));
        }
    }
}
