using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook;

namespace FacebookFanpageApp {
	public static class JsonObjectExtensions {
		public static bool GetBoolValue(this JsonObject data, string key)
		{
			bool result = false;
			object val;
			if (data.TryGetValue(key, out val))
			{
				bool b;
				if (bool.TryParse(val.ToString(), out b))
					result = b;
			}
			return result;
		}
		public static long GetLongValue(this JsonObject data, string key)
		{
			long result = 0L;
			object val;
			if (data.TryGetValue(key, out val))
			{
				long l;
				if (long.TryParse(val.ToString(), out l))
					result = l;
			}
			return result;
		}
		public static int GetIntValue(this JsonObject data, string key)
		{
			int result = 0;
			object val;
			if (data.TryGetValue(key, out val))
			{
				int i;
				if (int.TryParse(val.ToString(), out i))
					result = i;
			}
			return result;
		}
		public static string GetStringValue(this JsonObject data, string key)
		{
			string result = String.Empty;
			object val;
			if (data.TryGetValue(key, out val)) result = val.ToString();
			return result;
		}
	}
}