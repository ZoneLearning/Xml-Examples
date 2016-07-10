using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlDocument_OOP
{
    public class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        //Her bir nesneyi kendi içinde XMl ile tutma.
        //Tekrar döngüye sokabilmek için.
        //kaynagımı da içimde barındırıyorum
        public XmlNode Node { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname;

        }
    }
}
