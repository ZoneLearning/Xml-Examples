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

            SqlConnection connection = new SqlConnection("Server=.;Database=NORTHWND;;Integrated Security=true");
            SqlCommand command = new SqlCommand("select * from Categories", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            xwriter.WriteStartElement("Kategoriler");

            while (reader.Read())
            {
                xwriter.WriteStartElement("Kategori");
                xwriter.WriteElementString("Adi", reader["CategoryName"].ToString());
                xwriter.WriteElementString("Aciklama", reader["Description"].ToString());
                xwriter.WriteEndElement();
            }
            connection.Close();
            xwriter.WriteEndElement();
            xwriter.Close();
        }
    }
}
