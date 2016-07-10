using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XmlDocument_OOP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("../../Students.xml");
            XmlNode students = xdoc.SelectSingleNode("Students");
            XmlNodeList list = students.SelectNodes("Student");
            foreach (XmlNode student in list)
            {
                string name = student.Attributes["name"].Value;
                string surname = student.Attributes["surname"].Value;
                listBox1.Items.Add(name + " " + surname);
            }
        }
    }
}
