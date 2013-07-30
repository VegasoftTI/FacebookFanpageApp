using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Helpers;

namespace FacebookFanpageApp.Models
{
	public class FacebookModel
	{
		public MeModel Me { get; set; }

		public string App_Id
		{
			get
			{
				return ConfigurationManager.AppSettings["fb_app_id"];
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
			set { }
		}
		//public string CanvasUrl
		//{
		//    get
		//    {
		//        return ConfigurationManager.AppSettings["fb_canvasUrl"];
		//    }
		//    set { }
		//}
		//public string CanvasPage
		//{
		//	get
		//	{
		//		return ConfigurationManager.AppSettings["fb_canvasPage"];
		//	}
		//	set { }
		//}
		//public string RedirectUri
		//{
		//	get
		//	{
		//		return ConfigurationManager.AppSettings["fb_oauth_redirect"];
		//	}
		//	set { }
		//}
		public string State { get; set; }

		FbSignedRequest _signedRequestObject;
		public FbSignedRequest SignedRequestObject
		{
			get
			{
				return _signedRequestObject;
			}
			set
			{
				if (value != null)
					_signedRequestObject = Json.Decode(value.ToString(), typeof(FbSignedRequest));
			}
		}
	}
	public class FbSignedRequest
	{
		public string user { get; set; }
		public string algorithm { get; set; }
		public string issued_at { get; set; }
		public string user_id { get; set; }
		public string oauth_token { get; set; }
		public string expires { get; set; }

	}
}