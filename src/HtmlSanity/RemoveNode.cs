using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlSanity
{
    public class RemoveNode
    {
        public string nodeName;
        public string attrib;
        public string attribVal;
        public RemoveNode(string name, string attrib, string attribval)
        {
            this.nodeName = name;
            this.attrib = attrib;
            this.attribVal = attribval;
        }
    }
}
