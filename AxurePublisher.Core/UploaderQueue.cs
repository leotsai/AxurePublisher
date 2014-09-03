using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace AxurePublisher.Core
{
    public class UploaderQueue
    {
        private static readonly List<ChangingFile> _files;
        private static readonly List<ChangingFile> _errors; 
        private static readonly Timer _timer;
        private static object _lockObj = new object();
        private static bool _busy = false;

        static UploaderQueue()
        {
            _files = new List<ChangingFile>();
            _errors = new List<ChangingFile>();
            _timer = new Timer(5000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        public static void AddFile(ChangingFile file)
        {
            lock (_lockObj)
            {
                if (_files.Any(x => x.FullPath == file.FullPath))
                {
                    return;
                }
                _files.Add(file);
                Console.WriteLine(file.Name + " -> " + file.ChangeType);
            }
        }

        public static void AdError(ChangingFile error)
        {
            lock (_lockObj)
            {
                if (_errors.Any(x => x.FullPath == error.FullPath))
                {
                    return;
                }
                _errors.Add(error);
            }
        }

        private static void DoWork(Action callback)
        {
            lock (_lockObj)
            {
                if (_files.Count == 0)
                {
                    callback();
                    return;
                }
            }
            DoUploading(() =>
            {
                if (_errors.Count > 0)
                {
                    Console.WriteLine("Retry the errors? yes/no");
                    if (Console.ReadLine() == "yes")
                    {
                        _files.AddRange(_errors);
                    }
                    _errors.Clear();
                }
                callback();
            });
        }

        private static void DoUploading(Action callback)
        {
            var completeCounter = 0;
            var successCount = 0;
            for (var i = 0; i < AppContext.Threads; i++)
            {
                var thread = new System.Threading.Thread(() =>
                {
                    try
                    {
                        while (true)
                        {
                            ChangingFile file;
                            lock (_files)
                            {
                                file = _files.FirstOrDefault();
                                if (file != null)
                                {
                                    _files.Remove(file);
                                }
                            }
                            if (file == null)
                            {
                                completeCounter++;
                                if (completeCounter >= AppContext.Threads)
                                {
                                    Console.WriteLine("All threads completed.".ToUpper());
                                    Console.ForegroundColor = _errors.Count == 0 ? ConsoleColor.Green : ConsoleColor.Red;
                                    Console.WriteLine("Success: "+ successCount);
                                    Console.WriteLine("Errors: " + _errors.Count);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    callback();
                                }
                                break;
                            }
                            var client = new UploaderClient();
                            if (client.TryUpload(file))
                            {
                                successCount++;
                            }
                            else
                            {
                                AdError(file);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                });
                thread.Start();
            }
        }

        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_busy)
            {
                return;
            }
            _busy = true;
            try
            {
                DoWork(() =>
                {
                    _busy = false;
                });
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
