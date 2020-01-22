using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace GlobalTurnkey
{
    public static class Tools
    {
        public static Dictionary<String, String> GetDictionaryFromObject(Object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<Dictionary<String, String>>(json);
        }

        public static Dictionary<String, String> requestToDictionary(String requestString) {
            Dictionary<String, String> data = new Dictionary<String, String>();
            try
            {
                object[] kvp = requestString.Split('&');
                foreach (String s in kvp)
                {
                    object[] o = s.Split('=');
                    data.Add(o[0].ToString(), o[1].ToString());
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return data;
        }

        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static Dictionary<String, String> JsonToDictionary(String Json) {
            Dictionary<String, String> dic = new Dictionary<string, string>();

            JObject jsonObj = JObject.Parse(Json);
            foreach(var item in jsonObj)
            {
                string key = item.Key;
                string value = item.Value.ToString();

                dic.Add(key,value);
            }
            //Dictionary<String, String> dic = new Dictionary<string, string>();
            //String[] kvps = Json.Split(',');
            //for (int i = 0; i < kvps.Length; i++) {
            //    String[] kvp = kvps[i].Split(':');
            //    string k = string.Empty, v=string.Empty;
            //    if(kvp.Length>0)
            //       k = kvp[0].Trim().Replace("\\", "").Replace("\"", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
            //    if(kvp.Length>1)
            //       v = kvp[1].Trim().Replace("\\", "").Replace("\"", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
            //    dic.Add(k, v);
            //}
            return dic;
        }


    }
}