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
    public partial class müdür : Form
    {
        public müdür()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=kitaplik.mdb");
        string kimlik_no;
        void verileriGoster()
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM kullanicilar", baglanti);
            
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ogrenci_form frm = new ogrenci_form();
            frm.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu_metni = "INSERT INTO kullanicilar (k_adi,parola,adSoyad,yetki)" +
            "VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','"+ comboBox1.Text + "','"+ comboBox2.Text + "')";
            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand(sorgu_metni, baglanti);
            sorgu.ExecuteNonQuery();
     
            baglanti.Close();
            verileriGoster();
            MessageBox.Show("Üye Başarılı Bir Şekilde Eklendi");
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = ""; 

        }

        private void müdür_Load(object sender, EventArgs e)
        {
            verileriGoster();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            kimlik_no = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult basilan_buton;

            basilan_buton = MessageBox.Show("Silinecek Emin misin?", "UYARI !", MessageBoxButtons.YesNo);

            if (basilan_buton == DialogResult.Yes)
            {
                // Kimlik Alanına göre silme kodu
                string sorgu = "DELETE FROM kullanicilar WHERE Kimlik=" + kimlik_no;

                OleDbCommand sql_komut = new OleDbCommand(sorgu, baglanti);

                baglanti.Open();
                sql_komut.ExecuteNonQuery();
                baglanti.Close();

                verileriGoster();
            }
        }
    }
}
