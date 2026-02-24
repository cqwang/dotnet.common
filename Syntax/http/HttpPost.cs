using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    public partial class HttpIPC
    {
        public static string TestHttpPost(string uri, string request)
        {
            string response = "";
            HttpWebRequest webRequest = null;
            //Post请求地址
            try
            {
                webRequest = (HttpWebRequest)WebRequest.Create(uri);
                //相应请求的参数
                byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(request);
                webRequest.Method = "Post";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = data.Length;
                webRequest.Timeout = 300000;
                webRequest.ServicePoint.Expect100Continue = false;
                webRequest.KeepAlive = false;
                //写入请求流
                using (var requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }
                //获取响应流
                using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (var responseStream = webResponse.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
                        {
                            response = streamReader.ReadToEnd();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (webRequest != null)
                {
                    webRequest.Abort();
                }
            }
            return response;
        }

        public static string TestHttpPostJson(string uri, string requestJson)
        {
            string response = "";
            HttpWebRequest webRequest = null;
            //Post请求地址
            try
            {
                webRequest = (HttpWebRequest)WebRequest.Create(uri);
                //相应请求的参数
                byte[] data = Encoding.UTF8.GetBytes(requestJson);
                webRequest.Method = "Post";
                webRequest.ContentType = "application/json";
                webRequest.ContentLength = data.Length;
                webRequest.Timeout = 300000;
                //写入请求流
                using (var requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }
                //获取响应流
                using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (var responseStream = webResponse.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            response = streamReader.ReadToEnd();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (webRequest != null)
                {
                    webRequest.Abort();
                }
            }
            return response;
        }

        public static TResponse TestHttpPostJson<TRequest,TResponse>(string uri, TRequest request)
        {
            TResponse response = default(TResponse);
            HttpWebRequest webRequest = null;
            //Post请求地址
            try
            {
                var requestJson = JsonConvert.SerializeObject(request);
                webRequest = (HttpWebRequest)WebRequest.Create(uri);
                //相应请求的参数
                byte[] data = Encoding.UTF8.GetBytes(requestJson);
                webRequest.Method = "Post";
                webRequest.ContentType = "application/json";
                webRequest.ContentLength = data.Length;
                webRequest.Timeout = 300000;
                //写入请求流
                using (var requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }
                //获取响应流
                var responseJson = string.Empty;
                using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (var responseStream = webResponse.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            responseJson = streamReader.ReadToEnd();
                        }
                    }
                }
                response = JsonConvert.DeserializeObject<TResponse>(responseJson);
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    var responseMessage = string.Empty;
                    using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        responseMessage = streamReader.ReadToEnd();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                if (webRequest != null)
                {
                    webRequest.Abort();
                }
            }
            return response;
        }


        public static string TestHttpPostSoap(string url, string action, string param, int timeout)
        {
            var postBytes = Encoding.UTF8.GetBytes(param);
            var webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            webRequest.Timeout = timeout;
            webRequest.Method = "POST";
            webRequest.KeepAlive = false;
            webRequest.ContentType = "text/xml; charset=utf-8";
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36";
            webRequest.ContentLength = postBytes.Length;
            webRequest.Credentials = CredentialCache.DefaultCredentials;

            var result = string.Empty;
            try
            {
                using (Stream reqStream = webRequest.GetRequestStream())
                {
                    reqStream.Write(postBytes, 0, postBytes.Length);
                }

                using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (var responseStream = webResponse.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            result = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            finally
            {
                webRequest.Abort();
            }
            return result;
        }


    }
}
