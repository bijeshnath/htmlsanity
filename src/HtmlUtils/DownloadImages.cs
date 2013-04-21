using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;

namespace HtmlUtils
{
    public class DownloadImages
    {
        HtmlAgilityPack.HtmlDocument doc;
        string path;
        string link;
        public DownloadImages(string arglink, string argpath)
        {
            path = argpath;
            link = arglink;
            doc = new HtmlAgilityPack.HtmlDocument();

        }
        public HtmlDocument Document
        {
            get
            {
                return doc;
            }
        }

        public IDictionary<string, string> Download(String strHtml)
        {
            List<String> fileNames = new List<string>();

            doc.LoadHtml(strHtml);
            int i = 0;
            var images = new Dictionary<string, string>();
            HtmlNode curNode = doc.DocumentNode;
            i = downLoadImagesFromChildNodes(curNode, i, images);

            return images;

        }


        public IDictionary<string, string> Download()
        {
            List<String> fileNames = new List<string>();
            WebClient wc = new WebClient();
            string str = wc.DownloadString(link);
            doc.LoadHtml(str);
            int i = 0;
            var images = new  Dictionary<string, string>();
            HtmlNode curNode = doc.DocumentNode;
            i = downLoadImagesFromChildNodes(curNode, i, images );

            return images;

        }
        private int downLoadImagesFromChildNodes(HtmlNode node, int i, IDictionary<string, string> images)
        {

            if (node.HasChildNodes)
            {
                foreach (HtmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "img")
                    {
                        String src = childNode.GetAttributeValue("src", "---+++");
                        if (src != "---+++")
                        {
                            int qIndex = src.IndexOf('?');
                            if(qIndex>0)
                                src = src.Substring(0, qIndex);
                            string ext = System.IO.Path.GetExtension(src);
                            WebClient wc = new WebClient();
                            string outFile = path + "img" + i + ext;
                            wc.DownloadFile("http:" + src, outFile );
                            if(!images.ContainsKey(src))
                                images.Add(src, outFile);
                            childNode.SetAttributeValue("src", outFile);
                            i++;
                        }
                    }
                    i = downLoadImagesFromChildNodes(childNode, i, images);
                }

            }

            return i;
        }
    }
}
