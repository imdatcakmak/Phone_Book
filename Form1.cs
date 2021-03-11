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
namespace Rehber_Proje
{
    public partial class Form1 : Form
    {
        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-G2J4S8K\SQLEXPRESS;Initial Catalog=Rehber;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Kisiler", bgl);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void temizle()
        {
            TxtAd.Text = "";
            Txtid.Text = "";
            TxtMail.Text = "";
            TxtSoyad.Text = "";
            MskTel.Text = "";
            TxtAd.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("insert into tbl_kisiler (ad,soyad,telefon,maıl) values(@p1,@p2,@p3,@p4)", bgl);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTel.Text);
            komut.Parameters.AddWithValue("@p4", TxtMail.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kişi Rehbere Kaydedildi..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MskTel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("Delete from Tbl_Kisiler where ID=@p1", bgl);
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kişi rehberden silinmiştir..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("update tbl_kisiler set ad=@p1,soyad=@p2,telefon=@p3,maıl=@p4 where ID=@p5", bgl);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTel.Text);
            komut.Parameters.AddWithValue("@p4", TxtMail.Text);
            komut.Parameters.AddWithValue("@p5", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Kişi bilgisi güncellendi..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();

        }
    }
}
