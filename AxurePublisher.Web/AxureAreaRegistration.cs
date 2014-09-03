using System.Web.Mvc;

namespace AxurePublisher.Web
{
    public class AxureAreaRegistration : AreaRegistration
    {
        public const string Name = "Axure";

        public override string AreaName
        {
            get
            {
                return Name;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Axure_default",
                "Axure/{controller}/{action}/{id}",
                new { controller = "home", action = "Index", id = UrlParameter.Optional },
                new[] { "AxurePublisher.Web.Controllers.*" }
            );
        }
    }
}
