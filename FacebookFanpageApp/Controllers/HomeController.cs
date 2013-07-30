using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using FacebookFanpageApp.Models;

namespace FacebookFanpageApp.Controllers {
	public class HomeController : Controller {

		public ActionResult SetSession(string signedRequest)
		{
			if (!String.IsNullOrEmpty(signedRequest))
			{
				Session.Add("signedRequest", signedRequest);
				return View();
			}
			string rUrl = Url.Action("Firstpage","Home");
			return RedirectToAction("Firstpage","Home");
		}
		public ActionResult Firstpage()
		{
			string signedRequest = String.Empty;
			try
			{
				signedRequest = Session["signedRequest"].ToString();
			}
			catch { }
			if (String.IsNullOrEmpty(signedRequest))
				return View("NoSignedRequest");

			ViewBag.Message = "Facebook Fanpage App";
			FacebookModel model = new FacebookModel();
			FacebookClient client = new FacebookClient();
			client.AppSecret = model.App_Secret; dynamic signedRequestObject = null;
			if (client.TryParseSignedRequest(signedRequest, out signedRequestObject))
			{
				if (!String.IsNullOrEmpty(signedRequestObject.oauth_token))
				{
					FacebookClient authClient = new FacebookClient(signedRequestObject.oauth_token);
					model.Me = new MeModel((JsonObject)authClient.Get("/me"));
				}
			}
			return View(model);
		}
		public ActionResult NoSignedRequest()
		{
			FacebookModel model = new FacebookModel();
			return View(model);
		}

		public ActionResult ContinueConnected(string uid, string accessToken, string signedRequest)
		{
			//string signed_request = Session["signed_request"].ToString();
			ViewBag.AccessToken = accessToken;
			ViewBag.SignedRequest = signedRequest;
			ViewBag.Uid = uid;
			FacebookModel model = new FacebookModel();
			FacebookClient client = new FacebookClient(accessToken);
			client.AppSecret = model.App_Secret;
			dynamic signedRequestObject = null;
			if (client.TryParseSignedRequest(signedRequest, out signedRequestObject))
			{
				model.Me = new MeModel((JsonObject)client.Get("/me"));
				return View(model);
			}
			return View();
		}
	}
}
