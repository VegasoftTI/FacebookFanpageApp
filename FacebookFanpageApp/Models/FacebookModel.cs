using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Helpers;
using Facebook;

namespace FacebookFanpageApp.Models {
	public class FacebookModel {
		public MeModel Me { get; set; }
		public FbSignedRequest SignedRequest { get; set; }

		public string App_Id
		{
			get
			{
				return ConfigurationManager.AppSettings["fb_app_id"];
			}
			set { }
		}
		public string Scope
		{
			get
			{
				return ConfigurationManager.AppSettings["fb_scope"];
			}
			set { }
		}
		public string FanPageUrl
		{
			get
			{
				return ConfigurationManager.AppSettings["fb_fanpage_url"];
			}
			set { }
		}
		public string App_Secret
		{
			get
			{
				return ConfigurationManager.AppSettings["fb_app_secret"];
			}
			set { }
		}
		public string ChannelUrl
		{
			get
			{
				string path = ConfigurationManager.AppSettings["fb_channelPath"];
				Uri uri = HttpContext.Current.Request.Url;
				string url = String.Format("{0}://{1}{2}",
					uri.Scheme, uri.Authority, path);

				return url;
			}
		}


		//FbSignedRequest _signedRequestObject;
		//public FbSignedRequest SignedRequestObject
		//{
		//	get
		//	{
		//		return _signedRequestObject;
		//	}
		//	set
		//	{
		//		if (value != null)
		//			_signedRequestObject = Json.Decode(value.ToString(), typeof(FbSignedRequest));
		//	}
		//}
	}
	public class FbSignedRequest {
		public FBPage Page { get; set; }
		public FBUser User { get; set; }
		public long UserId { get; set; }
		public string Algorithm { get; set; }
		public DateTime IssuedAt { get; set; }
		public string OauthToken { get; set; }
		public DateTime Expires { get; set; }

		public FbSignedRequest(JsonObject data)
		{
			UserId = data.GetLongValue("user_id");
			Algorithm = data.GetStringValue("algorithm");
			OauthToken = data.GetStringValue("oauth_token");
			Expires = Facebook.DateTimeConvertor.FromUnixTime(data.GetStringValue("expires"));
			IssuedAt = Facebook.DateTimeConvertor.FromUnixTime(data.GetStringValue("issued_at"));
			object val = null;
			if (data.TryGetValue("user", out val))
			{
				User = new FBUser((JsonObject)val);
			}
			if (data.TryGetValue("page", out val))
			{
				Page = new FBPage((JsonObject)val);
			}
		}
	}
	//+		[2]	{"id":"129507630531720","liked":true,"admin":true}	object {Facebook.JsonObject}

	public class FBPage {
		public long Id { get; set; }
		public bool Liked { get; set; }
		public bool Admin { get; set; }
		public FBPage(JsonObject data)
		{
			Id = data.GetLongValue("id");
			Liked = data.GetBoolValue("liked");
			Admin = data.GetBoolValue("admin");
		}
	}
	public class FBAge {
		public int Min { get; set; }
		public FBAge(JsonObject data)
		{
			Min = data.GetIntValue("min");
		}
	}
	public class FBUser {
		public FBAge Age { get; set; }
		public string Country { get; set; }
		public string Locale { get; set; }
		public FBUser(JsonObject data)
		{
			object val = null;
			if (data.TryGetValue("age", out val))
			{
				Age = new FBAge((JsonObject)val);
			}
			Country = data.GetStringValue("country");
			Locale = data.GetStringValue("locale");
		}
	}

}