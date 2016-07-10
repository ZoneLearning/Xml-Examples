using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlDocument_OOP
{
    public class Exam
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
                //Bir xml elemanını tipini belirlemek icin--> Node.NodeType==XmlNodeType.Text vs
                //not attribute değil, o yüzden bir sonraki anlamına gelen NextSibling ya da FirstChild ile degeri alıyoruz.

                //if(Node.FirstChild!=null) diye kontrol etmk gerekiyor. Çünkü bazı node'lara değer girilmemiş olabilir.
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
