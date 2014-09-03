using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace AxurePublisher.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public string Index(string username, string password, string path)
        {
            if (username != "xxx" || password != "xxx")
            {
                return "403";
            }
            var file = Request.Files[0];
            if (file == null)
            {
                return "no";
            }
            var folder = ConfigurationManager.AppSettings["AxureStorageFolder"];
            var fullPath = (folder + path).Replace("/", "\\").Replace("\\\\", "\\");
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            var directory = fullPath.Substring(0, fullPath.LastIndexOf("\\"));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            file.SaveAs(fullPath);
            return "ok";
        }

    }
}
