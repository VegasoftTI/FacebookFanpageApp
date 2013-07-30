using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook;

namespace FacebookFanpageApp.Models {
	public class MeModel {
		public string Name { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public long Uid { get; set; }
		public string Email { get; set; }

		public MeModel() { }
		public MeModel(JsonObject data)
		{
			object val = String.Empty;
			if (data.TryGetValue("name", out val)) Name = val.ToString();
			if (data.TryGetValue("email", out val)) Email = val.ToString();
			if (data.TryGetValue("first_name", out val)) FirstName = val.ToString();
			if (data.TryGetValue("last_name", out val)) LastName = val.ToString();
		}
	}
}