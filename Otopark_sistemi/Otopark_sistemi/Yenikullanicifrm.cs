using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Otopark_sistemi
{
    public partial class Yenikullanicifrm : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Yenikullanicifrm()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
           this.Close();
        }
        private void temizle()
        {
            foreach (Control nesne in this.Controls)
            {
                if (nesne is TextBox)
                {
                    TextBox textbox = (TextBox)nesne;
                    textbox.Clear();
                }
            }
        }
                private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-UF9UBSE\\SQLEXPRESS;Initial Catalog=Otoparkdb;Integrated Security=True");//sql bağlantı kodu
            con.Open();
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand sorgula = new SqlCommand("select * from kullanicilar where kullaniciadi = '"+textBox1.Text+"'",con);
            SqlDataReader dr = sorgula.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Aynı isimli kullanıcı var..");
                temizle();
            }
            else if (textBox2.Text == textBox3.Text)
            {
                dr.Close();
                SqlCommand cmd = new SqlCommand("insert into kullanicilar(kullaniciadi, sifre) values('" +textBox1.Text+ "', '" +textBox2.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Yeni kullanıcı Eklendi…");
                con.Close();
            }
            else
            {
                MessageBox.Show("Şifreler aynı değil…");
            }
        }

        private void panelright_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}

