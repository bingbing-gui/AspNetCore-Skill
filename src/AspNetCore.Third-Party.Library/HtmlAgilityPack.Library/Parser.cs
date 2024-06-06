
using System;
using System.Xml;

namespace HtmlAgilityPack.Library
{
    internal class Parser
    {
        #region From File
        public static void LoadFromFile()
        {
            var html =
               @"<!DOCTYPE html>
                <html>
                    <body>
	                    <h1>This is <b>bold</b> heading</h1>
	                    <p>This is <u>underlined</u> paragraph</p>
	                    <h2>This is <i>italic</i> heading</h2>
                    </body>
                </html> ";
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            htmlDoc.Save("test.html");
            var path = @"test.html";
            var doc = new HtmlDocument();
            doc.Load(path);
            var htmlNode = doc.DocumentNode.SelectSingleNode("//body");
            Console.WriteLine(htmlNode.OuterHtml);
        }
        #endregion

        #region From String
        public static void LoadFromString()
        {
            var html =
             @"<!DOCTYPE html>
             <html>
                 <body>
	                 <h1>This is <b>bold</b> heading</h1>
	                 <p>This is <u>underlined</u> paragraph</p>
	                 <h2>This is <i>italic</i> heading</h2>
                 </body>
             </html> ";
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var htmlNode = htmlDoc.DocumentNode.SelectSingleNode("//body");
            Console.WriteLine(htmlNode.OuterHtml);
        }
        #endregion

        #region From Web
        public static void LoadFromWeb()
        {
            var html = @"https://html-agility-pack.net/";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var htmlNode = htmlDoc.DocumentNode.SelectSingleNode("//head/title");
            Console.WriteLine(htmlNode.OuterHtml);
        }
        #endregion

        #region From File rely on System.Windows.Forms namespace
        //public static void LoadFromBrowser()
        //{
        //    string url = "https://html-agility-pack.net/from-browser";

        //    var web1 = new HtmlWeb();
        //    var doc1 = web1.LoadFromBrowser(url, o =>
        //    {
        //        var webBrowser = (WebBrowser)o;

        //        // WAIT until the dynamic text is set
        //        return !string.IsNullOrEmpty(webBrowser.Document.GetElementById("uiDynamicText").InnerText);
        //    });
        //    var t1 = doc1.DocumentNode.SelectSingleNode("//div[@id='uiDynamicText']").InnerText;

        //    var web2 = new HtmlWeb();
        //    var doc2 = web2.LoadFromBrowser(url, html =>
        //    {
        //        // WAIT until the dynamic text is set
        //        return !html.Contains("<div id=\"uiDynamicText\"></div>");
        //    });
        //    var t2 = doc2.DocumentNode.SelectSingleNode("//div[@id='uiDynamicText']").InnerText;

        //    Console.WriteLine("Text 1: " + t1);
        //    Console.WriteLine("Text 2: " + t2);
        //}
        #endregion
    }
}
