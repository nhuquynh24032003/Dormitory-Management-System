using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace doanNet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DiemDanhKTXByTang",
                url: "DiemDanhKTX/{tang}",
                defaults: new { controller = "Default", action = "DiemDanhKTX", tang= UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "TrangChu", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "doanNet.Controllers" }

            );
        }
    }
}
