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

namespace XmlDocumentExample
{
    public partial class OrderDetailsForm : Form
    {
        public OrderDetailsForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Server=.;Database=NORTHWND;User=sa;Pwd=asdf");
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from [Order Details]", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            ds.WriteXml("order_detail.xml");

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml("order_detail.xml");
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
