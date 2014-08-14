using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;
using System.Web.Mvc;


namespace WebApplicationG16_2013
{
    public static class RouteConfig
    {
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    var settings = new FriendlyUrlSettings();
        //    settings.AutoRedirectMode = RedirectMode.Permanent;
        //    routes.EnableFriendlyUrls(settings);
        //}
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
