using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ButterflyNet.WebApi.Controllers;

namespace ButterflyNet.WebApi {

	public class NoSessionControllerFactory : DefaultControllerFactory {
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
			var controller = base.GetControllerInstance(requestContext, controllerType);
			((Controller)controller).TempDataProvider = new DummyTempDataProvider();
			return controller;
		}
	}


	public class DummyTempDataProvider : ITempDataProvider {
		public IDictionary<string, object> LoadTempData(ControllerContext controllerContext) {
			return new Dictionary<string, object>();
		}

		public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values) {
		}
	}


	public class MvcApplication : HttpApplication {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes) {
			ModelBinders.Binders[typeof(ButterflyController.ParseModel)] = new ButterflyController.ParseModelBinder();

			routes.MapRoute("default", "", new { controller = "Butterfly", action = "Index" });
			//routes.MapRoute("error", "{*anything}", new { controller = "Butterfly", action = "Error" });
		}

		protected void Application_Start() {
			ControllerBuilder.Current.SetControllerFactory(new NoSessionControllerFactory());
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			//set up logging
			log4net.Config.XmlConfigurator.Configure();
		}
	}
}