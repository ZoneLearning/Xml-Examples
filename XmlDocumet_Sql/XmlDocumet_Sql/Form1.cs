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
            SqlConnection conSuppliers = new SqlConnection("Server=.;Database=NORTHWND;Integrated Security=true;MultipleActiveResultSets=true");
            SqlCommand comSuppliers = new SqlCommand("Select * from Suppliers", conSuppliers);
            conSuppliers.Open();
            SqlDataReader suppliersRead = comSuppliers.ExecuteReader();
            while (suppliersRead.Read())
            {
                XmlElement supplier = xdoc.CreateElement("tedarikci");

                root.AppendChild(supplier);
            }
            conSuppliers.Close();

        }
    }
}
