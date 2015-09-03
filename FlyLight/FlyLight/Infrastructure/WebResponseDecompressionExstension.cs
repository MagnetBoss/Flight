using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace FlyLight.Infrastructure
{
    public static class WebResponseDecompressionExstension
    {
        public static Stream GetDecompressedStream(this WebResponse webResponse)
        {
            const string contentEncodingHeader = "Content-Encoding";
            if (!webResponse.Headers.AllKeys.Contains(contentEncodingHeader))
                return null;

            var contentEncoding = webResponse.Headers[contentEncodingHeader];
            if (string.IsNullOrEmpty(contentEncoding))
                return webResponse.GetResponseStream();
            
            Stream stream;

            switch (contentEncoding.ToUpperInvariant())
            {
                case "GZIP":
                    stream = new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress);
                    break;
                default:
                    stream = webResponse.GetResponseStream();
                    break;
            }
            return stream;
        }
    }
}
