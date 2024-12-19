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

namespace Kitaplik
{
    public partial class Giris : Form
    {

        public Giris()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=kitaplik.mdb");
        int sayac;
        int sayac2=10;

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sql = "SELECT * FROM kullanicilar WHERE k_adi ='" + textBox1.Text + "' AND parola = '" + textBox2.Text + "' ";
            OleDbCommand cmd = new OleDbCommand(sql, baglanti);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr.GetString(6) == "0")
                {
                    müdür mdr = new müdür();
                    mdr.ShowDialog();

                }
                else if (dr.GetString(6) == "1")
                {
                    Form1 form = new Form1();
                    form.ShowDialog();
                }
                else
                {
                    ogrenci_form frm = new ogrenci_form();
                    frm.giris_isim = textBox1.Text;
                    frm.ShowDialog();

                }
               
            }
            else
            {
                sayac++;
                MessageBox.Show("Kullanıcı Adı Veya Parola Yanlış ");
            }
            baglanti.Close();
            if (sayac == 3)
            {
                button1.Enabled = false;
                timer1.Enabled = true;
                MessageBox.Show("Çok Fazla Yanlış Deneme Yaptınız ");

            }


        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac2--;
            label3.Text = sayac2.ToString();
            if (sayac2 == 0)
            {
                button1.Enabled = true;
                timer1.Enabled = false;
                sayac = 0;
                sayac2 = 10;
                MessageBox.Show("Tekrar Deneyiniz");
                
            }
            


        }

        private void label4_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            sifremiunuttum sifre = new sifremiunuttum();
            sifre.ShowDialog();
            sifre.Location = giris.Location;

        }

        private void label5_Click(object sender, EventArgs e)
        {
            sifremiunuttum sifre = new sifremiunuttum();
            Giris giris = new Giris();
            sifre.ShowDialog();
            sifre.Location = giris.Location;
        }
    }
}
