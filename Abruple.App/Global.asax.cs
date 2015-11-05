using Parse;

namespace Abruple.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using System.Data.Entity;
    using App_Start;
    using Data;
    using Data.Migrations;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MapperConfig.ConfigureMappings();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ParseClient.Initialize("qFcw3fKZ3jB1jjse1PJmGOJ6NsPcW88EK43Ofixb", "rVGwZ4QQb0hLe9urvDHSq4F98YUOla7vTB64vsf6");
        }
    }
}
