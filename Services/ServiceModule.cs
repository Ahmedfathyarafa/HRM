using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace HRM.Services
{
   public class ServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerRequest();

            base.Load(builder); 
        }
    }
}
