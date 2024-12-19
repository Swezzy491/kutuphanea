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
    public partial class ogrenci_form : Form
    {
        public ogrenci_form()
        {
            InitializeComponent();
        }
        DateTime now = DateTime.Now;
        

        public string giris_isim {  get;   set;    }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=kitaplik.mdb");
        string kimlik_no;

        void verileriGoster()
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM kitaplar", baglanti);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void ogrenci_form_Load(object sender, EventArgs e)
        {
            verileriGoster();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            kimlik_no = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Eminmisin", "İşlem", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string sorgu_metni = "INSERT INTO ogrencikitap (ogrenci_adi,aldigi_ktb,kitabin_yazari,tarih)" +
              "VALUES ('" + giris_isim + "','" + textBox1.Text + "','" + textBox2.Text + "','" + now.ToString() + "')";

                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand(sorgu_metni, baglanti);
                sorgu.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("İşleminiz Başarılı Bir Şekilde Tamamlandı");
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("İşleminizi Tekrar Yapabilirsiniz");
            }

        }
    }
}
