using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook;

namespace FacebookFanpageApp.Models {
	/// <summary>
	/// {"id":"100002303587978",
	/// "name":"Dirk Seefeld",
	/// "first_name":"Dirk",
	/// "last_name":"Seefeld",
	/// "link":"https://www.facebook.com/dirk.seefeld.7",
	/// "username":"dirk.seefeld.7",
	/// "quotes":"Gandalf in Moria zu Frodo, als dieser sein Schicksal beklagt: ...",
	/// "work":[
	///		{
	///		"employer":{"id":"114363941913405", "name":"Freiberuflich/Selbstständig"},
	///		"start_date":"0000-00"
	///		}
	///	],
	/// "inspirational_people":[
	///		{"id":"17264938053","name":"J. R. R. Tolkien"},
	///		{"id":"105637189470679","name":"Ian Anderson"},
	///		{"id":"8570557485","name":"George Lucas"},
	///		{"id":"139980746073848","name":"Niels Hartvig"},
	///		{"id":"15650321041","name":"Salvador Dalí"},
	///		{"id":"108287242526144","name":"Arnold Böcklin"},
	///		{"id":"192742861478","name":"Steven Jobs"}
	///	],
	///	"education":[
	///			{"school": {"id":"102099239832297",
	///				"name":"Gymnasium Osterholz-Scharmbeck"},
	///				"year":{"id":"137801829576221","name":"1984"},
	///				"type":"High School"},
	///			{"school":{"id":"106187146087054",
	///				"name":"HBK Braunschweig"},
	///				"year":{"id":"145037408840681","name":"1991"},
	///				"concentration":[{"id":"109803049037749","name":"Graphic Design"}],
	///				"type":"College"}],
	///	"gender":"male",
	///	"email":"dirk.seefeld@idseefeld.de",
	///	"timezone":2,
	///	"locale":"de_DE",
	///	"languages":[
	///		{"id":"104254052943349","name":"Deutsch"},
	///		{"id":"107825489240781","name":"Englisch"}],
	///	"verified":true,
	///	"updated_time":"2013-07-30T13:11:16+0000"}
	/// </summary>
	public class MeModel {
		public string Name { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string Uid { get; set; }
		public string Email { get; set; }
		public string Link { get; set; }
		public string Quotes { get; set; }
		public string Gender { get; set; }
		public string Locale { get; set; }
		public string Timezone { get; set; }
		public List<Language> Languages { get; set; }

		public MeModel() { }
		public MeModel(JsonObject data)
		{
			object val = null;
			if (data.TryGetValue("timezone", out val)) Timezone = val.ToString();
			if (data.TryGetValue("locale", out val)) Locale = val.ToString();
			if (data.TryGetValue("gender", out val)) Gender = val.ToString();
			if (data.TryGetValue("name", out val)) Name = val.ToString();
			if (data.TryGetValue("email", out val)) Email = val.ToString();
			if (data.TryGetValue("first_name", out val)) FirstName = val.ToString();
			if (data.TryGetValue("last_name", out val)) LastName = val.ToString();
			if (data.TryGetValue("id", out val)) Uid = val.ToString();
			if (data.TryGetValue("username", out val)) Username = val.ToString();
			if (data.TryGetValue("link", out val)) Link = val.ToString();
			if (data.TryGetValue("quotes", out val)) Quotes = val.ToString();
			//complex data
			if (data.TryGetValue("languages", out val))
			{
				object sVal = null;
				List<Language> languages = new List<Language>();
				foreach (JsonObject item in (JsonArray)val)
				{
					Language lang = new Language(item);
					if (lang.IsValid)
					{
						languages.Add(lang);
					}
				}
				Languages = languages;
			}

		}
	}
	public class Language {
		public string Id { get; set; }
		public string Name { get; set; }
		public bool IsValid
		{
			get
			{
				return !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Id);
			}
			private set { }
		}
		public Language(JsonObject data)
		{
			object val = null;
			if (data.TryGetValue("id", out val)) Id = val.ToString();
			if (data.TryGetValue("name", out val)) Name = val.ToString();
		}
	}
}