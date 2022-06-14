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
    public partial class frmAracOtoparkKaydı : Form
    {
        public frmAracOtoparkKaydı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-UF9UBSE\\SQLEXPRESS;Initial Catalog=Otoparkdb;Integrated Security=True");


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmAracOtoparkKaydı_Load(object sender, EventArgs e)
        {
            BosAraclar();
        }

        private void BosAraclar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from arac_durumu WHERE durumu='BOŞ'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox3.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into arac_otopark_kaydı(isim,soyisim,telefon,plaka,marka,model,renk,parkyeri,tarih)values(@isim,@soyisim,@telefon,@plaka,@marka,@model,@renk,@parkyeri,@tarih)", baglanti);
            komut.Parameters.AddWithValue("@isim",textBox1.Text.ToString());
            komut.Parameters.AddWithValue("@soyisim",textBox2.Text.ToString());
            komut.Parameters.AddWithValue("@telefon",textBox3.Text.ToString());
            komut.Parameters.AddWithValue("@plaka",textBox4.Text.ToString());
            komut.Parameters.AddWithValue("@marka",textBox5.Text.ToString());
            komut.Parameters.AddWithValue("@model",textBox6.Text.ToString());
            komut.Parameters.AddWithValue("@renk",textBox7.Text.ToString());
            komut.Parameters.AddWithValue("@parkyeri",comboBox3.Text.ToString());
            komut.Parameters.AddWithValue("@tarih", textBox8.Text.ToString());


            komut.ExecuteNonQuery();

            SqlCommand komut2 = new SqlCommand("update arac_durumu set durumu= 'DOLU' where parkyeri='"+comboBox3.SelectedItem+"'",baglanti);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Araç Kaydı Oluşturuldu");
            comboBox3.Items.Clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
