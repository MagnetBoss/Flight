using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using xBrainLab.Security.Cryptography;

namespace FlyLight.BL.ProposalsList.Implementation
{
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

        private const string ApiToken = "08fe1910088a2a5638939991eaf4a59e";

        public static string GenerateSignature(JObject requestBody)
        {
            var request = new StringBuilder();
            Recursive(request, requestBody);

            var md5Input = ApiToken + request;

            return MD5.GetHashString(md5Input);
        }
    }
}
