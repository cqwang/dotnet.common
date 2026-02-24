using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotnet.common.Syntax
{
    partial class Spider
    {
        public static void TestWebBrowser()
        {
            WebBrowser web = new WebBrowser();
            web.Navigate("http://www.xjflcp.com/ssc/");//从指定网站下载数据
            web.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(web_DocumentCompleted);
        }

        private static void web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //数据处理
            WebBrowser web = (WebBrowser)sender;
            HtmlElementCollection ElementCollection = web.Document.GetElementsByTagName("Table");

            //数据存储
            foreach (HtmlElement item in ElementCollection)
            {
                File.AppendAllText("Kaijiang_xj.txt", item.InnerText);
            }
        }
    }
}
