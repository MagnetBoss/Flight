using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using xBrainLab.Security.Cryptography;

namespace FlyLight.BL.ProposalsList.Implementation.TravelPayouts
{
    //Cоздаем signature ключ по алгоритму, указанному на сайте travelpayouts
    //см. https://support.travelpayouts.com/hc/ru/articles/203956173?_ga=1.45153312.583613381.1440156926
    public class RequestSignatureGenerator
    {
        private static void Recursive(StringBuilder request, JToken json)
        {
            if (json is JValue)
            {
                request.Append(((JValue)json).Value + ":");
                return;
            }

            if (json is JObject)
            {
                foreach (var property in ((JObject)json).Properties().OrderBy(p => p.Name))
                {
                    Recursive(request, property.Value);
                }
                return;
            }

            if (json is JArray)
            {
                foreach (var element in ((JArray)json).Children())
                {
                    Recursive(request, element);
                }
                return;
            }
        }

        public static string GenerateSignature(JObject requestBody, string apiToken)
        {
            var request = new StringBuilder();
            Recursive(request, requestBody);
            var requestStr = request.ToString();

            var md5Input = apiToken + ":" + requestStr.Substring(0, requestStr.Length - 1);

            return MD5.GetHashString(md5Input).ToLower();
        }
    }
}
