using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using InventoryManagement.AppContext;
using InventoryManagement.Models.MapperProfile;
using InventoryManagement.Repository;

namespace InventoryManagement.App_Start
{
	public class AutofacConfig
	{
    public static void Register()
    {
      var builder = new ContainerBuilder();
      var config = GlobalConfiguration.Configuration;
      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
      RegisterServices(builder);
      builder.RegisterWebApiFilterProvider(config);
      builder.RegisterWebApiModelBinderProvider();
      var container = builder.Build();
      config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
    }

    private static void RegisterServices(ContainerBuilder builder)
    {
      var cnfg = new MapperConfiguration(cf =>
      {
        cf.AddProfile(new CategoryMappingProfile());
        cf.AddProfile(new InventoryItemMappingProfile());
      });
      builder.RegisterInstance(cnfg.CreateMapper())
        .SingleInstance();
      
      builder.RegisterType<CategoryRepository>()
        .As<ICategoryRespository>()
        .InstancePerRequest();
      builder.RegisterType<InventoryContext>()
      .InstancePerRequest();

    }
  }
}