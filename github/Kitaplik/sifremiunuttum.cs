using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kitaplik
{
    public partial class sifremiunuttum : Form
    {
        public sifremiunuttum()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=kitaplik.mdb");

        void verileriGoster()
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM kullanicilar", baglanti);

            DataTable dt = new DataTable();
            da.Fill(dt);

            baglanti.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        groupBox2.Visible = true;
        button4.Visible = false;
        button3.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
         groupBox1.Visible = true;
         button3.Visible = false;
         button4.Visible = false;
         groupBox1.Location = groupBox2.Location;
        }

        private void sifremiunuttum_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox6.Text != "" && textBox5.Text != "" && textBox7.Text != "")
            {

                string sorgu_metni = "INSERT INTO Kullanicilar (k_adi,parola,telefon,eposta,adSoyad,yetki)" +
                    "VALUES ('" + textBox3.Text + "','" + textBox6.Text + "','" + textBox5.Text + "','" + textBox7.Text + "','Ögrenci','2')";

                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand(sorgu_metni, baglanti);
                sorgu.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarılı Bir Şekilde Tamamlandı ");
                Giris giris = new Giris();
                giris.ShowDialog();
            }
            else
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sql = "SELECT * FROM kullanicilar WHERE k_adi ='" + textBox1.Text + "' AND telefon = '" + textBox4.Text + "' AND eposta = '" + textBox2.Text + "' ";
            OleDbCommand cmd = new OleDbCommand(sql, baglanti);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show(dr.GetString(2));
                Giris giris = new Giris();
                giris.ShowDialog();
            }
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox1.Visible = true;
            groupBox1.Location = groupBox2.Location;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox1.Location = groupBox2.Location;
        }
    }
}
