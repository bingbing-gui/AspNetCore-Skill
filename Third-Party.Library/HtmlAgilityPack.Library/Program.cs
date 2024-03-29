
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
    //给我创建一个订单类
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public string Comments { get; set; }
    }

}