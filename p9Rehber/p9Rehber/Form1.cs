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

namespace p9Rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-23T2RIK\\SQLEXPRESS;Initial Catalog=p9Rehber;Integrated Security=True");

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select*from Table_Kisiler",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void temizle()
        {
            txtAD.Text = "";
            txtID.Text = "";
            txtMAIL.Text = "";
            txtSOYAD.Text = "";
            mskTELEFON.Text = "";
            txtAD.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into Table_Kisiler (AD,SOYAD,TELEFON,MAIL) values (@p1,@p2,@p3,@p4)", baglanti);
            cmd.Parameters.AddWithValue("@p1", txtAD.Text);
            cmd.Parameters.AddWithValue("@p2", txtSOYAD.Text);
            cmd.Parameters.AddWithValue("@p3", mskTELEFON.Text);
            cmd.Parameters.AddWithValue("@p4", txtMAIL.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Başarıyla Rehberinize Eklendi!","İşlem Başarılı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAD.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSOYAD.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            mskTELEFON.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtMAIL.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd1 = new SqlCommand("delete from Table_Kisiler where ID="+txtID.Text, baglanti);
            
            DialogResult dialog = new DialogResult();
            dialog= MessageBox.Show("Kişiyi Rehberinizden silinecek! Bunu Onaylıyor Musunuz?", "Emin misiniz?", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Kişi Başarıyla Rehberinizden Silindi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
            }
            baglanti.Close();
            listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("update Table_Kisiler set AD=@p1,SOYAD=@p2,TELEFON=@p3,MAIL=@p4 where ID=@p5", baglanti);
            cmd2.Parameters.AddWithValue("@p1", txtAD.Text);
            cmd2.Parameters.AddWithValue("@p2", txtSOYAD.Text);
            cmd2.Parameters.AddWithValue("@p3", mskTELEFON.Text);
            cmd2.Parameters.AddWithValue("@p4", txtMAIL.Text);
            cmd2.Parameters.AddWithValue("@p5", txtID.Text);
            cmd2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Başarıyla Güncellendi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
    }
}
