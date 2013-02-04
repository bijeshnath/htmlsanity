using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;

namespace HtmlSanity
{
    public class HtmlSanitiser
    {
        HtmlAgilityPack.HtmlDocument doc;

        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="thelink">url to download the Html from</param>
        public HtmlSanitiser(string thelink)
        {

            doc = new HtmlAgilityPack.HtmlDocument();
            WebClient wc = new WebClient();
            string str = wc.DownloadString(thelink);
            doc.LoadHtml(str);
        }

        public HtmlSanitiser(string html, bool asString)
        {
            doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
        }


        /// <summary>
        /// Retrieve the Html as a string
        /// </summary>
        public string Html
        {
            get
            {
                return doc.DocumentNode.InnerHtml;
            }
        }
        private string title;
        public string Title
        {
            get
            {
                if (title != null)
                    return title;
                else
                {
                    setTitle(doc.DocumentNode);
                    return title;
                }
            }
        }

        private void setTitle(HtmlNode node)
        {
            if (!lookForTitle(node) && node.HasChildNodes)
            {
                foreach (HtmlNode childNode in node.ChildNodes)
                {
                    setTitle(childNode);
                }
            }
        }
        private bool lookForTitle(HtmlNode node)
        {
            if (node.Name == "title")
            {
                title = node.InnerText;
                return true;
            }
            return false;
        }
        public void RemoveNodes(RemoveNode[] nodesToRemove)
        {

            foreach (RemoveNode nodeToRemove in nodesToRemove)
            {
                removeNodes(doc.DocumentNode, nodeToRemove.nodeName, nodeToRemove.attrib, nodeToRemove.attribVal, true);
            }
        }


        private void removeNodes(HtmlNode node, string name, string attrib, string attribval, bool processChildNodes)
        {
            removeNodesFromChildren(node, name, attrib, attribval);
            if (processChildNodes && node.HasChildNodes)
            {
                foreach (HtmlNode childNode in node.ChildNodes)
                {
                    removeNodes(childNode, name, attrib, attribval, processChildNodes);
                }
            }
        }


        private void removeNodesFromChildren(HtmlNode node, string name, string attrib, string attribval)
        {

            if (node.HasChildNodes)
            {
                List<HtmlNode> nodesToRemove = new List<HtmlNode>();
                foreach (HtmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == name)
                    {
                        if (attrib != null && attribval != null)
                        {
                            if (childNode.GetAttributeValue(attrib, "----++") == attribval)
                                nodesToRemove.Add(childNode);
                        }
                        else
                        {
                            nodesToRemove.Add(childNode);
                        }
                    }
                }
                foreach (HtmlNode childNode in nodesToRemove)
                {
                    node.ChildNodes.Remove(childNode);
                }
            }
        }

    }
}
