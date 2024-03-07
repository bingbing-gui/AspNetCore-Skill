
using System;
using System.Xml.Linq;

namespace HtmlAgilityPack.Library
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Parser
            #region 从文件中加载HTML
            var htmlNode = Parser.LoadFromFile();

            Console.WriteLine(htmlNode.OuterHtml);
            #endregion

            #region 从字符创中加载HTML
            htmlNode = Parser.LoadFromString();
            Console.WriteLine(htmlNode.OuterHtml);
            #endregion

            #region 从web地址中加载HTML
            htmlNode = Parser.LoadFromWeb();

            Console.WriteLine(htmlNode.OuterHtml);
            #endregion

            #endregion 
            #region Selectors
            
            var htmlNodes = Selectors.SelectNodes();
            foreach (var node in htmlNodes)
            {
                Console.WriteLine(node.Attributes["value"].Value);
            }

            htmlNode = Selectors.SelectSingleNode();
            var value = htmlNode.Attributes["value"].Value;
            Console.WriteLine(value);
            
            #endregion


            Console.ReadLine();
        }

    }
}