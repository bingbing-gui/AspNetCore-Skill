using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAgilityPack.Library
{
    internal class Selectors
    {
        public static void SelectNodes()
        {
            var html =
         @"<TD class=texte width=""50%"">
			<DIV align=right>Name :<B> </B></DIV>
		</TD>
		<TD width=""50%"">
    		<INPUT class=box value=John maxLength=16 size=16 name=user_name>
    		<INPUT class=box value=Tony maxLength=16 size=16 name=user_name>
    		<INPUT class=box value=Jams maxLength=16 size=16 name=user_name>
		</TD>
		<TR vAlign=center>";

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//td/input");

            foreach (var node in htmlNodes)
            {
                Console.WriteLine(node.Attributes["value"].Value);
            }
        }
        public static void SelectSingleNode()
        {
            var html =
    @"<TD class=texte width=""50%"">
			<DIV align=right>Name :<B> </B></DIV>
		</TD>
		<TD width=""50%"">
    		<INPUT class=box value=John maxLength=16 size=16 name=user_name>
    		<INPUT class=box value=Tony maxLength=16 size=16 name=user_name>
    		<INPUT class=box value=Jams maxLength=16 size=16 name=user_name>
		</TD>
		<TR vAlign=center>";

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var value = htmlDoc.DocumentNode
                .SelectSingleNode("//td/input")
                .Attributes["value"].Value;

            Console.WriteLine(value);
        }
    }
}
