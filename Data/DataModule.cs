using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Data;
using HRM.Data.Repository;

namespace HRM.Data
{
   public class DataModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            // register repository
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(EfContext)).As(typeof(DbContext)).InstancePerRequest();

            base.Load(builder);
        }
    }
}
