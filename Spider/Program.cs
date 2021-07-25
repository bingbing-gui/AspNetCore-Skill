using HtmlAgilityPack;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
namespace Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            var rates=GetExchangeRateBySpider();

            Console.WriteLine(rates);
            Console.ReadLine();
        }
        public static decimal GetExchangeRateBySpider()
        {
            decimal rate = 0.0m;
            HtmlWeb web = new HtmlWeb();
            //加载Html DOM 树
            HtmlDocument doc = web.Load(@"http://fx.cmbchina.com/hq/");
            //SelectSingleNode 方法需要XPath 表达式
            var htmlNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"realRateInfo\"]");
            if (htmlNode != null)
            {
                var htmlNodes = htmlNode.SelectSingleNode(@"table").SelectNodes(@"tr");
                for (int r = 0; r < htmlNodes.Count; r++)
                {
                    if (r == 8)
                    {
                        var htmltds = htmlNodes[r].SelectNodes(@"td");
                        for (int c = 0; c <= htmltds.Count; c++)
                        {
                            if (c == 3)
                            {
                                var ret = decimal.TryParse(htmltds[c].InnerText.Trim(), out rate);
                            }
                        }
                    }
                }
            }
            return rate;
        }

    }
}