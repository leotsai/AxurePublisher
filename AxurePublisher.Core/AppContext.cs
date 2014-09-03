using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AxurePublisher.Core
{
    public class AppContext
    {
        private static readonly string _watchingFolder;
        private static readonly int _threads;
        private static readonly string _url;

        public static string WatchingFolder
        {
            get { return _watchingFolder; }
        }

        public static int Threads
        {
            get { return _threads; }
        }

        public static string Url
        {
            get { return _url; }
        }

        static AppContext()
        {
            var settings = ConfigurationManager.AppSettings;
            _watchingFolder = settings["WatchingFolder"];
            _threads = int.Parse(settings["Threads"]);
            _url = settings["Url"];
        }
    }
}
