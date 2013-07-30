using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using FacebookFanpageApp.Models;

namespace FacebookFanpageApp.Controllers
{
	public class FacebookController : Controller
	{
		//
		// GET: /idseefeld-fb/Facebook/

		public ActionResult Index()
		{
			return View("Error");
		}

		[HttpPost]
		public ActionResult Index(string signed_request)
		{
			FacebookModel model = new FacebookModel();
			FacebookClient fbClient = new FacebookClient();
			fbClient.AppSecret = model.App_Secret;
			dynamic signedRequestObject = null;
			if (fbClient.TryParseSignedRequest(signed_request, out signedRequestObject))
			{
				bool isAnonymous = true;
				JsonObject jObj = (JsonObject)signedRequestObject;
				isAnonymous = !jObj.Keys.Contains("user_id");
				if (isAnonymous)
				{
					model.State = new Guid().ToString();
					Session["fbState"] = model.State;
					return View("AnonymousUser", model);
				}
				else
				{
					//signed_request kommt auch für anonyme user!
					return View(model);
				}
			}
			else
			{
				return View("Error");
			}
		}
		public ActionResult Noaccess()
		{
			return View("Noaccess");
		}
	}
}
