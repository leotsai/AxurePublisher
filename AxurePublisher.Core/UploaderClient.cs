using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AxurePublisher.Core
{
    public class UploaderClient
    {
        public bool TryUpload(ChangingFile file)
        {
            if (!File.Exists(file.FullPath))
            {
                return true;
            }
            try
            {
                using (var client = new WebClient())
                {
                    var url = AppContext.Url + "?username=xxx&password=xxx&path=" + file.ServerPath;
                    client.UploadFile(url, file.FullPath);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("uploaded " + file.Name);
                    Console.ResetColor();
                    return true;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("error " + file.Name);
                Console.ResetColor();
                return false;
            }
        }
    }
}
