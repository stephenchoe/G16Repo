using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using AutoMapper;

namespace G16MG
{
    //public static class Bootstrapper
    //{
    //    public static void Run()
    //    {
    //        SetAutofacContainer();
    //        //Configure AutoMapper
    //        AutoMapperConfiguration.Configure();
    //    }
    //    private static void SetAutofacContainer()
    //    {
    //        var builder = new ContainerBuilder();
    //        builder.RegisterControllers(Assembly.GetExecutingAssembly());
    //        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
    //        builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerHttpRequest();
    //        builder.RegisterAssemblyTypes(typeof(FocusRepository).Assembly)
    //        .Where(t => t.Name.EndsWith("Repository"))
    //        .AsImplementedInterfaces().InstancePerHttpRequest();
    //        builder.RegisterAssemblyTypes(typeof(GoalService).Assembly)
    //       .Where(t => t.Name.EndsWith("Service"))
    //       .AsImplementedInterfaces().InstancePerHttpRequest();

    //        builder.RegisterAssemblyTypes(typeof(DefaultFormsAuthentication).Assembly)
    //     .Where(t => t.Name.EndsWith("Authentication"))
    //     .AsImplementedInterfaces().InstancePerHttpRequest();

    //        builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SocialGoalEntities())))
    //            .As<UserManager<ApplicationUser>>().InstancePerHttpRequest();

    //        builder.RegisterFilterProvider();
    //        IContainer container = builder.Build();
    //        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    //    }
    //}
}