using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CourseLearner
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Course",
               url: " {controller}/{action}/{CourseId}",
               defaults: new { action = "Index", CourseId = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "SearchCourse",
               url: "controller/{action}/{searchTerm}",
               defaults: new { action = "FilterResult", searchTerm = UrlParameter.Optional }
           );
         routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}