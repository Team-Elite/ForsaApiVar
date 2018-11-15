using System.Web.Mvc;

namespace ForsaWebAPI.Areas.ForsaAPIHelp
{
    public class ForsaAPIHelpAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ForsaAPIHelp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ForsaAPIHelp_default",
                "ForsaAPIHelp/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}