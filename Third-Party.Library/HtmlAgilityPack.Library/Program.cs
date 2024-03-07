
using System;
using System.Xml.Linq;

namespace HtmlAgilityPack.Library
{
    class Program
    {
        static void Main(string[] args)
        {

            //#region Parser 从文件,字符创,web地址中加载HTML
            //Parser.LoadFromFile();
            //Parser.LoadFromString();
            //Parser.LoadFromWeb();
            //#endregion 
            //#region Selectors
            //Selectors.SelectNodes();
            //Selectors.SelectSingleNode();
            //#endregion
            //Manipulation.UsageExampleOfHtmlNodeProperties();

            //Manipulation.AppendChild();
            //Manipulation.AppendChildren();
            Manipulation.Clone();
            Console.ReadLine();
        }

    }
}