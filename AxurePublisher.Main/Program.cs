using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AxurePublisher.Core;

namespace AxurePublisher.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Core.Publisher.Start();
            Console.Write("Started watching ".ToUpper());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[{0}]", AppContext.WatchingFolder);
            Console.ResetColor();
            Console.WriteLine("... ...");
            while (true)
            {
                Console.Read();
            }
        }
    }
}
