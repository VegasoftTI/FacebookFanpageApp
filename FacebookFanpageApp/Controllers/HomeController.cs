using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using FacebookFanpageApp.Models;

namespace FacebookFanpageApp.Controllers {
	public class HomeController : Controller {
		/// <summary>
		/// Take the signed_request data and store it for further use in session which is now available
		/// with or without cookies
		/// </summary>
		/// <param name="signedRequest">signed_request as received from inital Facebook POST request</param>
		/// <returns>redirect to first view</returns>
		public ActionResult SetSession(string signedRequest)
		{
			if (!String.IsNullOrEmpty(signedRequest))
			{
				Session.Add("signedRequest", signedRequest);
			}
			return RedirectToAction("Firstpage", "Home");
		}
		/// <summary>
		/// Evalueate signed request and showing Login / Permisson button or 
		/// displaying Facebook data
		/// </summary>
		/// <returns>View</returns>
		public ActionResult Firstpage()
		{
			ViewBag.Message = "Facebook Fanpage App";

			string signedRequest = Session["signedRequest"]!=null ? 
				Session["signedRequest"].ToString() : String.Empty;

			if (String.IsNullOrEmpty(signedRequest))
				return RedirectToAction("NoSignedRequest");

			FacebookModel model = new FacebookModel();
			FacebookClient client = new FacebookClient();
			client.AppSecret = model.App_Secret;
			dynamic signedRequestObject = null;
			if (client.TryParseSignedRequest(signedRequest, out signedRequestObject))
			{
				model.SignedRequest = new FbSignedRequest(signedRequestObject);
			}
			if (!String.IsNullOrEmpty(model.SignedRequest.OauthToken))
			{
				FacebookClient authClient = new FacebookClient(model.SignedRequest.OauthToken);
				try
				{
					JsonObject data = (JsonObject)authClient.Get("/me");
					model.Me = new MeModel(data);
				}
				catch
				{
					//e. g. user loged aout or session terminates
					return RedirectToAction("Offline");
				}
			}
			if (model.SignedRequest.User.Locale.Equals("de_de", StringComparison.InvariantCultureIgnoreCase))
			{
				System.Threading.Thread.CurrentThread.CurrentCulture =
					System.Threading.Thread.CurrentThread.CurrentUICulture =
					new CultureInfo("de-DE");
			}
			return View(model);
		}
		public ActionResult Offline()
		{
			ViewBag.FaceBookPageUrl = ConfigurationManager.AppSettings["fb_fanpage_url"].ToString();
			return View();
		}

		/// <summary>
		/// Default route outside Facebook iframe
		/// </summary>
		/// <returns>redirect to Facebook Tab Page</returns>
		public ActionResult NoSignedRequest()
		{
			bool fb_show_app_start_view_without_facebook = false;
			bool.TryParse(ConfigurationManager.AppSettings["fb_show_app_start_view_without_facebook"],
				out fb_show_app_start_view_without_facebook);

			FacebookModel model = new FacebookModel();
			if (fb_show_app_start_view_without_facebook)
				return View(model);
			else
				return Redirect(model.FanPageUrl);
		}
		/// <summary>
		/// Here goes the redirect from FacebookHttpHandler of CustomFacebookHandler see RouteConfig.cs
		/// after POST request from Facebook
		/// </summary>
		/// <param name="signedRequest">Facebook signed_request</param>
		/// <returns>View with js redirect</returns>
		
		
	}
}
