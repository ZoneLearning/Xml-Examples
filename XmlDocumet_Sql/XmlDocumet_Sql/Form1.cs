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
            /*Connection pooling, veritabanına yapılan bağlantıların sonraki bağlantılar için de sıfırdan oluşturma maliyetine gerek kalmaksızın tekrar kullanılabilir olması amacıyla bellekte (sunucuda hani hostta) tutulan bir tür saklayıcıdır. */

            /* Her bir connection pool için min pool size ve max pool size özellikleri verilir. Bu değerlerin varsayılan değerleri sırasıyla 1 ve 100'dür. Yani varsayılan değerler bırakılırsa connection pool 100 adede kadar aktif bağlantı tutar. Veritabanına yeni bir bağlantı talebi gelirse, havuzda boşta bağlantı varsa hemen verilir. (Max Pool sayısı, ihtiyaca göre değişir. Pool kapasitesini artırırsanız daha çok talebe hızlı cevap verebilirsiniz 
             * fakat bir yandan da bellekte daha fazla yer kaplamaya başlayacağınızdan, bu noktada ince ayar yapılarak optimal değerler bulunmalıdır.)*/

            XmlDocument xdoc = new XmlDocument();
            XmlElement root = xdoc.CreateElement("Suppliers");
            SqlConnection conSuppliers = new SqlConnection("Server=.;Database=NORTHWND;Integrated Security=true;MultipleActiveResultSets=true;Max Pool Size=500;Pooling=true");
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

                SqlCommand comProduct = new SqlCommand("Select * from Products where SupplierId=@s_id", conSuppliers);
                comProduct.Parameters.AddWithValue("@s_id", suppliersRead["SupplierId"]);
                SqlDataReader productReader = comProduct.ExecuteReader();
                while (productReader.Read())
                {
                    XmlElement product = xdoc.CreateElement("product");
                    XmlAttribute name = xdoc.CreateAttribute("name");
                    name.Value = productReader["ProductName"].ToString();
                    XmlAttribute price = xdoc.CreateAttribute("price");
                    price.Value = productReader["UnitPrice"].ToString();
                    XmlAttribute stock = xdoc.CreateAttribute("stock");
                    stock.Value = productReader["UnitsInStock"].ToString();

                    product.Attributes.Append(name);
                    product.Attributes.Append(price);
                    product.Attributes.Append(stock);

                    supplier.AppendChild(product);
                }

                root.AppendChild(supplier);
            }
            conSuppliers.Close();

            xdoc.AppendChild(root);
            xdoc.Save("suppliers.xml");

        }
    }
}
