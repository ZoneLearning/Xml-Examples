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

namespace XmlDocumet_Sql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("Tedarikciler");
            SqlConnection conSuppliers = new SqlConnection("Server=.;Database=NORTHWND;Integrated Security=true;MultipleActiveResultSets=true;");
            SqlCommand comSuppliers = new SqlCommand("Select * from Suppliers", conSuppliers);
            conSuppliers.Open();
            SqlDataReader suppliersRead = comSuppliers.ExecuteReader();
            while (suppliersRead.Read())
            {
                XmlElement supplier = xdoc.CreateElement("supplier");
                XmlAttribute company = xdoc.CreateAttribute("company");
                company.Value = suppliersRead["CompanyName"].ToString();
                XmlAttribute phone = xdoc.CreateAttribute("phone");
                phone.Value = suppliersRead["Phone"].ToString();
                XmlAttribute contactName = xdoc.CreateAttribute("contact");
                contactName.Value = suppliersRead["ContactName"].ToString();

                supplier.Attributes.Append(company);
                supplier.Attributes.Append(phone);
                supplier.Attributes.Append(contactName);

                root.AppendChild(supplier);
            }
            conSuppliers.Close();

            xdoc.AppendChild(root);
            xdoc.Save("suppliers.xml");

        }
    }
}
