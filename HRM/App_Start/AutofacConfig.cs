using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using HRM.Data;
using HRM.Services;

namespace HRM.Web.App_Start
{
    public class AutofacConfig
    {
        /// <summary>
        /// Creates a new container with the component registrations that have been made.
        /// </summary>
        public static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            // Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();
            // Register Modules
            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new ServiceModule());

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}