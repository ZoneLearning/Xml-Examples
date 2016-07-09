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

namespace XmlDocumentExample
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
            xdoc.Load("..\\..\\products.xml");
            XmlNode products = xdoc.SelectSingleNode("products");
            textBox1.Text = products.Attributes["supplier"].Value;
            textBox2.Text = products.Attributes["branch_office"].Value;
            foreach (XmlNode product in products.SelectNodes("product"))
            {
                ListViewItem list = new ListViewItem();
                list.Text = product.SelectSingleNode("name").InnerText;
                list.SubItems.Add(product.SelectSingleNode("price").InnerText);
                list.SubItems.Add(product.SelectSingleNode("stock").InnerText);
                listView1.Items.Add(list);

            }
        }
    }
}
