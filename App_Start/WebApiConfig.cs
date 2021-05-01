using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using InventoryManagement.App_Start;
using Newtonsoft.Json.Serialization;

namespace InventoryManagement
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			AutofacConfig.Register();

			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			// Web API routes
			config.MapHttpAttributeRoutes();
		}
	}
}
