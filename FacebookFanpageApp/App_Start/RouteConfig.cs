using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FacebookFanpageApp {
	public class RouteConfig {
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			//it is important to set a fake RouteValueDictionary for defaults and constraints
			//otherwise the following MapRoute entry do not work
			routes.Add(
				new Route("start",
					new RouteValueDictionary() { { "controller", "qwewqewqeqweq" } },
					new RouteValueDictionary() { { "controller", "qwewqewqeqweq" } },
					new FacebookFanpageApp.CustomFacebookHandler())
			);
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "NoSignedRequest", id = UrlParameter.Optional }
			);
		}
	}
}