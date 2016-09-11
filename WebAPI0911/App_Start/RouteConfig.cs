using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAPI0911
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //* => Catch All  後面的方入pathInfo
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //有加入後就可以在MVC的Action上使用[route]功能
            //routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
