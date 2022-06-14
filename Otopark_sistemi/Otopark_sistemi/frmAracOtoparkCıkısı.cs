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

namespace Otopark_sistemi
{
    public partial class frmAracOtoparkCıkısı : Form
    {
        public frmAracOtoparkCıkısı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-UF9UBSE\\SQLEXPRESS;Initial Catalog=Otoparkdb;Integrated Security=True");

        private void frmAracOtoparkCıkısı_Load(object sender, EventArgs e)
        {
            DoluYerler();
            Plakalar();
            timer1.Enabled = true;
        }

        private void Plakalar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from arac_otopark_kaydı",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox4.Items.Add(read["plaka"].ToString());
            }
            baglanti.Close();
        }

        private void DoluYerler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from arac_durumu where durumu='DOLU'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox3.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from arac_otopark_kaydı where plaka='"+comboBox4.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox14.Text = read["parkyeri"].ToString();
                    }
            baglanti.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from arac_otopark_kaydı where parkyeri='" + comboBox3.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox16.Text = read["parkyeri"].ToString();
                textBox9.Text = read["isim"].ToString();
                textBox11.Text = read["soyisim"].ToString();
                textBox13.Text = read["marka"].ToString();
                textBox12.Text = read["model"].ToString();
                textBox10.Text = read["renk"].ToString();
                textBox8.Text = read["plaka"].ToString();
                lblGirisTarihi.Text = read ["tarih"].ToString();

                
            }
            baglanti.Close();
            DateTime gelis, cıkıs;
            gelis = DateTime.Parse(lblGirisTarihi.Text);
            cıkıs = DateTime.Parse(lblCıkısTarihi.Text);
            TimeSpan fark;
            fark = cıkıs - gelis;
            lblSure.Text = fark.TotalHours.ToString("0.00");
            lblToplamTutar.Text = (double.Parse(lblSure.Text) * (0.75)).ToString("0.00");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from arac_otopark_kaydı where plaka='"+textBox8.Text+"'", baglanti);
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update arac_durumu set durumu='BOŞ' where parkyeri='" + textBox16.Text + "'", baglanti);
            komut2.ExecuteNonQuery();
            SqlCommand komut3 = new SqlCommand("insert into satis_bilgileri(parkyeri,plaka,giris_tarihi,cıkıs_tarihi,süre,tutar) values(@parkyeri,@plaka,@giris_tarihi,@cıkıs_tarihi,@süre,@tutar)", baglanti);
            komut3.Parameters.AddWithValue("@parkyeri", textBox16.Text);
            komut3.Parameters.AddWithValue("@plaka", textBox8.Text);
            komut3.Parameters.AddWithValue("@giris_tarihi", lblGirisTarihi.Text);
            komut3.Parameters.AddWithValue("@cıkıs_tarihi", lblCıkısTarihi.Text);
            komut3.Parameters.AddWithValue("@süre", double.Parse(lblSure.Text));
            komut3.Parameters.AddWithValue("@tutar", double.Parse(lblToplamTutar.Text));
            komut3.ExecuteNonQuery(); 
            baglanti.Close();
            MessageBox.Show("Araç çıkışı yapıldı");
            foreach(Control item in groupBox5.Controls)
            {
                if (item is TextBox)
                   
                {
                    item.Text = "";
                    textBox14.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                }
            }
            comboBox4.Items.Clear();
            comboBox3.Items.Clear();
            DoluYerler();
            Plakalar();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblCıkısTarihi.Text = DateTime.Now.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
