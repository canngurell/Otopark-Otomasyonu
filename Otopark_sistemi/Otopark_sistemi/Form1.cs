using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//sql bağlantısı için gerekli kod

namespace Otopark_sistemi
{
    public partial class FrmAdminGiris : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public FrmAdminGiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM kullanicilar where kullaniciadi=@user AND sifre=@pass";
            con = new SqlConnection("Data Source=DESKTOP-UF9UBSE\\SQLEXPRESS;Initial Catalog=Otoparkdb;Integrated Security=True");
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@user", textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", textBox2.Text);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Tebrikler! Başarılı bir şekilde giriş yaptınız.");
                Anasayfafrm anasayfafrm = new Anasayfafrm();
                anasayfafrm.Show();  // form2 göster diyoruz
                this.Hide();   // bu yani form1 gizle diyoruz
            }
            else
            {
                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
            }
            con.Close();

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Yenikullanicifrm yenikullanicifrm = new Yenikullanicifrm();
            yenikullanicifrm.Show();  // form2 göster diyoruz
        }

        private void panelright_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
