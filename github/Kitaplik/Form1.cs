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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=kitaplik.mdb");
        string kimlik_no;

        void verileriGoster()
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM kitaplar",baglanti);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        void ogrencileriGoster()
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM ogrencikitap", baglanti);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView2.DataSource = dt;
            baglanti.Close();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            verileriGoster();
            ogrencileriGoster();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Yeni Kitap Ekle butonu
            string sorgu_metni = "INSERT INTO kitaplar (adi,yazar,sayfa_sayisi,tur,konum)" +
                "VALUES ('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+comboBox1.Text+"','"+textBox5.Text+"')";

            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand(sorgu_metni,baglanti);
            sorgu.ExecuteNonQuery();
            baglanti.Close();

            verileriGoster();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Güncelle Butonu
            string sorgu_metni = "UPDATE kitaplar " +
                "SET adi = '"+textBox8.Text+"',yazar='"+textBox7.Text+"',sayfa_sayisi='"+textBox6.Text+"',tur='"+comboBox2.Text+"',konum='"+textBox4.Text+"'" +
                "WHERE Kimlik="+kimlik_no;

            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand(sorgu_metni, baglanti);
            sorgu.ExecuteNonQuery();
            baglanti.Close();

            verileriGoster();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            kimlik_no = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // SİL Butonu
            DialogResult basilan_buton;

            basilan_buton = MessageBox.Show("Silinecek Emin misin?", "UYARI !", MessageBoxButtons.YesNo);

            if (basilan_buton == DialogResult.Yes)
            {
                // Kimlik Alanına göre silme kodu
                string sorgu = "DELETE FROM kitaplar WHERE Kimlik=" + kimlik_no;

                OleDbCommand sql_komut = new OleDbCommand(sorgu, baglanti);

                baglanti.Open();
                sql_komut.ExecuteNonQuery();
                baglanti.Close();

                verileriGoster();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
