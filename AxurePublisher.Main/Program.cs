using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AxurePublisher.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Core.Publisher.Start();
            Console.WriteLine("Started watching...".ToUpper());
            while (true)
            {
                Console.Read();
            }
        }
    }
}
