using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlDocument_OOP
{
    public class Project
    {
        public string Name
        {
            get
            {

                return Node.Attributes["name"].Value;
            }
        }
        public double Grade
        {
            get
            {
                if (Node.FirstChild != null)
                    return double.Parse(Node.FirstChild.Value);
                return 0;
            }
        }
        public XmlNode Node { get; set; }

        public override string ToString()
        {
            return Name + "-" + Grade;
        }
    }
}
