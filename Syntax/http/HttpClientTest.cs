using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotnet.common.Syntax
{
    /// <summary>
    /// 更简洁、通用的http通信
    /// 支持异步、非阻塞
    /// </summary>
    class HttpClientTest
    {
        public async Task TestAsync()
        {
            string uri = "http://www.baidu.com/";
            HttpClient client = new HttpClient();
            string body = await client.GetStringAsync(uri);
        }

        public async Task<Product> Test()
        {
            string uri = "http://www.baidu.com/";
            Product product = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        public async void HttpClientDoGet()
        {
            var uri = "http://api.wsncloud.com/device/v1/list?";
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };

            using (var httpclient = new HttpClient(handler))
            {
                httpclient.BaseAddress = new Uri(uri);
                httpclient.DefaultRequestHeaders.Accept.Clear();
                httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpclient.GetAsync("?ak=xxxxxxxxxxxxxxxxxxxx");

                if (response.IsSuccessStatusCode)
                {
                    Stream myResponseStream = await response.Content.ReadAsStreamAsync();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();

                    MessageBox.Show(retString);
                }
            }
        }

        public async void HttpClientDoPost()
        {
            var uri = "http://api.wsncloud.com/sensor/v1/list?";
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };

            using (var httpclient = new HttpClient(handler))
            {
                httpclient.BaseAddress = new Uri(uri);
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"ak", "65fc7ca4fc441d26f71bf3d691b278c2"},
                    {"deviceId", "537eb34be4b022b7fbe19471"}
                });

                var response = await httpclient.PostAsync(uri, content);

                string responseString = await response.Content.ReadAsStringAsync();
                MessageBox.Show(responseString);
            }
        }

        
    }

    class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
    }
}
