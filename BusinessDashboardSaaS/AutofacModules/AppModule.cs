//using Autofac;
//using BusinessDashboardSaaS.Data.Interfaces;
//using BusinessDashboardSaaS.Data.Repositories;
//using BusinessDashboardSaaS.Services;
//namespace BusinessDashboardSaaS.AutofacModules
//{
//    public class AppModule: Module
//    {
//        public static void RegisterServices(ContainerBuilder builder)
//        {
//            // Register repositories
//            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
//            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();

//        }
//    }

//    }


using Autofac;
using BusinessDashboardSaaS.Data.Interfaces;
using BusinessDashboardSaaS.Data.Repositories;
using BusinessDashboardSaaS.Services;

namespace BusinessDashboardSaaS.AutofacModules
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductRepository>()
                   .As<IProductRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>()
                   .As<IProductService>()
                   .InstancePerLifetimeScope();

            // Register other repositories/services as needed
        }
    }
}

