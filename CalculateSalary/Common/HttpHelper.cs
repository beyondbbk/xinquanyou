using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace MySoft.Common
{
    public class HttpHelper
    {
        /// <summary>
        /// 发送Post请求，拿到数据
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postDataStr">要post的json数据</param>
        /// <param name="requestEncodeType">请求的编码方式</param>
        /// <param name="responseEncodeType">响应的编码方式</param>
        /// <param name="keepAlive">设置Alive</param>

        /// <returns></returns>
        public static string Post(string url, string postDataStr, string requestEncodeType = "UTF-8", string responseEncodeType = "UTF-8", bool keepAlive = true)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.KeepAlive = keepAlive;
            var postData = Encoding.GetEncoding(requestEncodeType).GetBytes(postDataStr);
            var myRequestStream = request.GetRequestStream();
            myRequestStream.Write(postData, 0, postData.Length);
            myRequestStream.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var myResponseStream = response.GetResponseStream();
            if (myResponseStream != null)
            {
                var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(responseEncodeType));
                var retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            return null;
        }
        public static string Get(string url, string getDataStr, string requestEncodeType = "UTF-8", string responseEncodeType = "UTF-8", bool keepAlive = true)
        {
            var request = (HttpWebRequest)WebRequest.Create($"{url}?{getDataStr}");
            LogHelper.Debug("请求地址信息："+ $"{url}?{getDataStr}");
            request.Method = "Get";
            //request.ContentType = "application/json";
            //request.KeepAlive = keepAlive;

            var response = (HttpWebResponse)request.GetResponse();
            var myResponseStream = response.GetResponseStream();
            if (myResponseStream != null)
            {
                var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(responseEncodeType));
                var retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            return null;
        }

        public static string HttpGet(string url)
        {
            HttpClient httpClient = new HttpClient();
            var t = httpClient.GetByteArrayAsync(url);
            t.Wait();
            var ret = Encoding.UTF8.GetString(t.Result);
            return ret;
        }
    }
}
