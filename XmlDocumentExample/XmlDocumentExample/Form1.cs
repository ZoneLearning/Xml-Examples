using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            xdoc.Load("products_db.xml");
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Server=.;Database=NORTHWND;User=sa;Pwd=asdf");
            SqlCommand command = new SqlCommand("Select * from Products", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            XmlDocument doc = new XmlDocument();
            XmlElement products = doc.CreateElement("products");
            doc.AppendChild(products);

            XmlAttribute supplier = doc.CreateAttribute("supplier");
            supplier.Value = "Bim";

            XmlAttribute branch_office = doc.CreateAttribute("branch_office");
            branch_office.Value = "Esentepe-Kartal";
            products.Attributes.Append(supplier);
            products.Attributes.Append(branch_office);

            while (reader.Read())
            {
                XmlElement product = doc.CreateElement("product");

                XmlElement name = doc.CreateElement("name");
                name.InnerText = reader["ProductName"].ToString();

                XmlElement price = doc.CreateElement("price");
                doc.CreateElement("price");
                price.InnerText = reader["UnitPrice"].ToString();

                XmlElement stock = doc.CreateElement("stock");
                doc.CreateElement("stock");
                stock.InnerText = reader["UnitsInStock"].ToString();


                product.AppendChild(name);
                product.AppendChild(price);
                product.AppendChild(stock);

                products.AppendChild(product);
            }
            connection.Close();
            doc.Save("products_db.xml");
        }

        private void btnIntentOrderDetail_Click(object sender, EventArgs e)
        {
            OrderDetailsForm form = new OrderDetailsForm();
            form.Show();
        }
    }
}
