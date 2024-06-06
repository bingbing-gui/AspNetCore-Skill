using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HtmlAgilityPack.Library
{
    internal class Manipulation
    {
        public static void UsageExampleOfHtmlNodeProperties()
        {
            var html =
                @"<body>
                    <h1>This is <b>bold</b> heading</h1>
                    <p>This is <u>underlined</u> paragraph</p>
			
			        <h1>This is <i>italic</i> heading</h1>
			        <p>This is <u>underlined</u> paragraph</p>
                </body>";
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//body/h1");
            Console.WriteLine("---------------------InnerHtml Property---------------------");
            foreach (var node in htmlNodes)
            {
                Console.WriteLine(node.InnerHtml);
            }
            Console.WriteLine("---------------------InnerText Property---------------------");
            foreach (var node in htmlNodes)
            {
                Console.WriteLine(node.InnerText);
            }
            Console.WriteLine("---------------------OuterHtml Property---------------------");
            foreach (var node in htmlNodes)
            {
                Console.WriteLine(node.OuterHtml);
            }
            Console.WriteLine("---------------------ParentNode Name---------------------");
            HtmlNode parentNode = htmlDoc.DocumentNode.SelectSingleNode("//body/h1").ParentNode;
            Console.WriteLine(parentNode.Name);
            Console.WriteLine("---------------------End---------------------");
        }

        public static void AppendChild()
        {
            var html =
                @"<body>
                    <h1>This is <b>bold</b> heading</h1>
                    <p>This is <u>underlined</u> paragraph</p>
                 </body>";
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");
            HtmlNode h2Node = HtmlNode.CreateNode("<h2> This is h2 heading</h2>");
            htmlBody.AppendChild(h2Node);
            Console.WriteLine("\n****After child node appended****\n");
        }
        public static void AppendChildren()
        {
            var html =
                @"<body>
                    <h1>This is <b>bold</b> heading</h1>
                    <p>This is <u>underlined</u> paragraph</p>
                </body>";
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            Console.WriteLine(htmlDoc.DocumentNode.OuterHtml);
            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");
            HtmlNode h2Node = HtmlNode.CreateNode("<h2> This is h2 heading</h2>");
            HtmlNode pNode1 = HtmlNode.CreateNode("<p> This is appended paragraph 1</p>");
            HtmlNode pNode2 = HtmlNode.CreateNode("<p> This is appended paragraph 2</p>");
            HtmlNodeCollection children = new HtmlNodeCollection(htmlBody);
            children.Add(h2Node);
            children.Add(pNode1);
            children.Add(pNode2);
            htmlBody.AppendChildren(children);
            Console.WriteLine("\n****After children appended****\n");
            Console.WriteLine(htmlDoc.DocumentNode.OuterHtml);
        }
        public static void Clone()
        {
            var html =
                @"<body>
                    <h1>This is <b>bold</b> heading</h1>
                    <p>This is <u>underlined</u> paragraph</p>
                 </body>";
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");
            HtmlNode newHtmlBody = htmlBody.Clone();
            Console.WriteLine(newHtmlBody.OuterHtml);
        }

        //public static void CloneNode()
        //{


        //}
        ///// <summary>
        ///// 创建节点副本的同时改变它的名字
        ///// </summary>
        ///// <param name=""></param>
        //public static void CloneNode(String name)
        //{

        //}
        ///// <summary>
        ///// 创建节点副本的同时改变它的名字
        ///// </summary>
        ///// <param name=""></param>
        ///// <param name=""></param>
        //public static void CloneNode(String, Boolean)
        //{


        //}
        ///// <summary>
        ///// 创建节点以及子节点的副本
        ///// </summary>
        ///// <param name=""></param>
        //public static void CopyFrom(HtmlNode htmlNode)
        //{


        //}
        ///// <summary>
        /////  创建一个节点的副本
        ///// </summary>
        ///// <param name=""></param>
        ///// <param name=""></param>
        //public static void CopyFrom(HtmlNode, Boolean)
        //{


        //}
        ///// <summary>
        ///// Creates an HTML node from a string representing literal HTML.
        ///// </summary>
        //public static void CreateNode()
        //{

        //}
        ///// <summary>
        ///// Inserts the specified node immediately after the specified reference node.
        ///// </summary>
        //public static void InsertAfter()
        //{


        //}
        ///// <summary>
        ///// Inserts the specified node immediately before the specified reference node.
        ///// </summary>
        //public static void InsertBefore()
        //{


        //}
        ///// <summary>
        /////  Adds the specified node to the beginning of the list of children of this node.
        ///// </summary>
        //public static void PrependChild()
        //{


        //}
        ///// <summary>
        ///// Adds the specified node list to the beginning of the list of children of this node.
        ///// </summary>
        //public static void PrependChildren()
        //{


        //}
        ///// <summary>
        ///// Removes node from parent collection
        ///// </summary>
        //public static void Remove()
        //{


        //}
        ///// <summary>
        /////  Removes all the children and/or attributes of the current node.
        ///// </summary>
        //public static void RemoveAll()
        //{

        //}
        ///// <summary>
        ///// Removes all the children of the current node.
        ///// </summary>
        //public static void RemoveAllChildren()
        //{

        //}
        ///// <summary>
        ///// Removes the specified child node.
        ///// </summary>
        ///// <param name=""></param>
        //public static void RemoveChild(HtmlNode)
        //{


        //}
        ///// <summary>
        ///// Removes the specified child node.
        ///// </summary>
        ///// <param name=""></param>
        ///// <param name=""></param>
        //public static void RemoveChild(HtmlNode, Boolean)
        //{


        //}
        ///// <summary>
        /////  Replaces the child node oldChild with newChild node.
        ///// </summary>
        //public static void ReplaceChild()
        //{


        //}
    }
}
