using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HtmlAgilityPack;

namespace HtmlSanity
{
    [TestFixture]
    public class Test
    {
        [TestCase]
        public static void TestRemoveNodeFromUrl()
        {
            String url = "http://www.bbc.co.uk/news/";
            HtmlSanitiser sn = new HtmlSanitiser(url);
            RemoveNode rn1 = new RemoveNode("script", null, null);
            sn.RemoveNodes(new RemoveNode[] { rn1 });

            string htmlout = sn.Html;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlout);

            bool foundNode = false;
            if (doc.DocumentNode.Name == "script")
                foundNode = true;
            if(doc.DocumentNode.HasChildNodes)
            {
                foreach(HtmlNode cNode in doc.DocumentNode.ChildNodes)
                    if (cNode.Name == "script")
                    {
                        foundNode = true;
                    }
            }

            if (htmlout.Contains("<script"))
            {
                foundNode = true;
            }

            Assert.AreEqual(foundNode, false);
        }
    }
}
