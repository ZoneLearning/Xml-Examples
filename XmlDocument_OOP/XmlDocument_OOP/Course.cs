using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlDocument_OOP
{
    public class Course
    {
        public string Name
        {
            get
            {

                return Node.Attributes["name"].Value;
            }
        }
        public int Credit
        {
            get
            {
                return int.Parse(Node.Attributes["credit"].Value);
            }
        }
        public int Period
        {
            get
            {
                return int.Parse(Node.Attributes["period"].Value);
            }
        }
        public XmlNode Node { get; set; }

        public override string ToString()
        {
            return Name + " " + Credit + " " + Period;
        }
    }


}
