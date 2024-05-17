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

namespace OgrenciOtomasyon
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        public Form1()
        {
            InitializeComponent();
        }

        void OgrenciGetir()
        {
            baglanti= new SqlConnection("server=.;Initial Catalog=OgrenciOtomasyon;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT*FROM OGRENCİLER", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            OgrenciGetir();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO OGRENCİLER(OgrenciIsim,OgrenciSoyisim,TCno,Telno,Bolum) VALUES (@OgrenciIsim,@OgrenciSoyisim,@TCno,@Telno,@Bolum)";
            komut=new SqlCommand(sorgu,baglanti);

            komut.Parameters.AddWithValue("@OgrenciIsim", nameTextBox.Text);
            komut.Parameters.AddWithValue("@OgrenciSoyisim",surnameTextBox.Text);
            komut.Parameters.AddWithValue("@TCno",TCnoTextBox.Text);
            komut.Parameters.AddWithValue("@Telno",PhoneNuTextBox.Text);
            komut.Parameters.AddWithValue("@Bolum",departmentTextBox.Text);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close() ;
            OgrenciGetir();
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            nameTextBox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            surnameTextBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TCnoTextBox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            PhoneNuTextBox.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            departmentTextBox.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM OGRENCİLER WHERE TCno=@TCno";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@TCno", Convert.ToInt64(TCnoTextBox.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            OgrenciGetir() ;
        }
    }
}
