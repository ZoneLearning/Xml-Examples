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

namespace XmlTextWriterExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnXMLWrite_Click(object sender, EventArgs e)
        {
            XmlTextWriter xwriter = new XmlTextWriter("Categories.xml", Encoding.Unicode);

            //Birden çok bağlantı var ise - >MultipleActiveResultSets=True
            //Windows Auhentication ile baglantı olusturmak icin -> Integrated Security=true
            SqlConnection connection = new SqlConnection("Server=.;Database=NORTHWND;Integrated Security=true;MultipleActiveResultSets=True");

            SqlConnection connection2 = new SqlConnection("Server=.;Database=NORTHWND;Integrated Security=true;MultipleActiveResultSets=True");

            SqlCommand command = new SqlCommand("select * from Categories", connection);
            //Kategori id'se sahip olan ürünleri sıra sıra yazdırmakiçin
            SqlCommand command2 = new SqlCommand("select * from Products where CategoryID=@c_id", connection2);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            xwriter.WriteStartElement("Kategoriler");

            while (reader.Read())
            {
                xwriter.WriteStartElement("Kategori");
                xwriter.WriteElementString("Adi", reader["CategoryName"].ToString());
                xwriter.WriteElementString("Aciklama", reader["Description"].ToString());

                /*Clear() ile temizlemediğimizde hata veriyor. -->{"The variable name '@c_id' has already been declared.
                Variable names must be unique within a query batch or stored procedure."}*/
                command2.Parameters.Clear();
                command2.Parameters.AddWithValue("@c_id", reader["CategoryId"]);
                connection2.Open();

                SqlDataReader readerProduct = command2.ExecuteReader();
                while (readerProduct.Read())
                {
                    xwriter.WriteStartElement("Urun");
                    xwriter.WriteElementString("Adi", readerProduct["ProductName"].ToString());
                    xwriter.WriteElementString("Fiyat", readerProduct["UnitPrice"].ToString());
                    xwriter.WriteElementString("Stok", readerProduct["UnitsInStock"].ToString());
                    xwriter.WriteEndElement();
                }
                connection2.Close();
                xwriter.WriteEndElement();
            }
            connection.Close();
            xwriter.WriteEndElement();
            xwriter.Close();
        }
    }
}
