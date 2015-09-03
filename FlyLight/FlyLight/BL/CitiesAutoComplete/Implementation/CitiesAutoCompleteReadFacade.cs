using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FlyLight.BL.CitiesAutoComplete.Implementation
{
    public class CitiesAutoCompleteReadFacade : ICitiesAutoCompleteReadFacade
    {
        public async Task<List<string>> GetCities(string term, string locale)
        {
            string url = "http://places.aviasales.ru/?term=" + term + "&locale=" + locale;

            var request = (HttpWebRequest) WebRequest.Create(new Uri(url));
            request.Method = "GET";

            var response =
                await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);

            //request.Headers["Accept-Encoding"] = "gzip, deflate";

            var places = new List<string>();

            using (var stream = response.GetResponseStream())
            {
                var raw = new StreamReader(stream).ReadToEnd();
                dynamic d = JObject.Parse("{\"root\": " + raw + "}");

                for (var i = 0; i < (int) d.root.Count; ++i)
                {
                    places.Add(d.root[i].name.ToString());
                }
            }

            return places;
        }
    }
}
