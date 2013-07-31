using System;
using System.Configuration;
using System.Web;
using System.Web.Routing;

namespace FacebookFanpageApp {
	public class CustomFacebookHandler : IRouteHandler {
		public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return new FacebookHttpHandler(requestContext);
		}
	}
	/// <summary>
	/// We need to intercept the redirect of System.Web.Routing in case of cookieless session.
	/// This is the case for facebook pages if the browser does not allow third party cookies.
	/// </summary>
	public class FacebookHttpHandler : IHttpHandler {
		protected RequestContext RequestContext { get; set; }
		public FacebookHttpHandler() : base() { }
		public FacebookHttpHandler(RequestContext requestContext)
		{
			this.RequestContext = requestContext;
		}

		#region IHttpHandler Members

		public bool IsReusable
		{
			get { return false; }
		}

		public void ProcessRequest(HttpContext context)
		{
			string signed_request = context.Request.Form["signed_request"];
			if (!String.IsNullOrEmpty(signed_request))
			{
				context.Response.Redirect(String.Format(
					"{0}?signedRequest={1}",
					ConfigurationManager.AppSettings["fb_initial_post_request_redirect"],
					signed_request));
			}
		}

		#endregion
	}
}