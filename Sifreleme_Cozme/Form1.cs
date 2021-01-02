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
using System.Collections;
using Entity;

namespace Sifreleme_Cozme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection bgl = new SqlConnection(@"Data Source = DESKTOP-INSQH5J\SQLEXPRESS; Initial Catalog = Test; Integrated Security = True");
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            SifreCozme();
        }
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TBLVERILER",bgl);
         
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.RowHeadersVisible = false;
        }
        void temizle()
        {
            txtad.Text ="";
            txthesapno.Text = "";
            txtmail.Text = "";
            txtsifre.Text = "";
            txtsoyad.Text = "";

               
        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtad.Text!=""&&txthesapno.Text!=""&&txtmail.Text!=""&&txtsifre.Text!=""&&txtsoyad.Text!="")
            {
            string ad = txtad.Text;
            byte[] addizi = ASCIIEncoding.ASCII.GetBytes(ad);
            string adsifre = Convert.ToBase64String(addizi);

            string soyad = txtsoyad.Text;
            byte[] soyaddizi = ASCIIEncoding.ASCII.GetBytes(soyad);
            string soyadsifre = Convert.ToBase64String(soyaddizi);

            string mail = txtmail.Text;
            byte[] maildizi = ASCIIEncoding.ASCII.GetBytes(mail);
            string mailsifre = Convert.ToBase64String(maildizi);

            string sifre = txtsifre.Text;
            byte[] sifredizi = ASCIIEncoding.ASCII.GetBytes(sifre);
            string sifresifre = Convert.ToBase64String(sifredizi);

            string hesapno = txthesapno.Text;
            byte[] hesapnodizi = ASCIIEncoding.ASCII.GetBytes(hesapno);
            string hesapnosifre = Convert.ToBase64String(hesapnodizi);
            bgl.Open();
            SqlCommand komut = new SqlCommand("insert into TBLVERILER (AD,SOYAD,MAIL,SIFRE,HESAPNO) VALUES (@P1,@P2,@P3,@P4,@P5)",bgl);
            komut.Parameters.AddWithValue("@p1",adsifre);
            komut.Parameters.AddWithValue("@p2",soyadsifre);
            komut.Parameters.AddWithValue("@p3",mailsifre);
            komut.Parameters.AddWithValue("@p4",sifresifre);
            komut.Parameters.AddWithValue("@p5",hesapnosifre);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("VERİ EKLENDİ.");
            listele();
            SifreCozme();
                temizle();
            }
            else
            {
                MessageBox.Show("LÜTFEN BİLGİLERİ EKSİKSİZ GİRİNİZ.");
            }
        }

        string idno;
        private void SifreCozme()
        {// SİFRELİ VERİLERİ ÇÖZME
            bgl.Open();
            List<EntityLayer> list = new List<EntityLayer>();
            SqlCommand komut =new SqlCommand("select*from TBLVERILER", bgl);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                EntityLayer entity = new EntityLayer();
                entity.ID1 = int.Parse(dr[0].ToString());
               
                string ad = dr[1].ToString();
                byte[] adcozumdizi = Convert.FromBase64String(ad);
                entity.ADI = ASCIIEncoding.ASCII.GetString(adcozumdizi);


                string soyad = dr[2].ToString();
                byte[] soyadcozumdizi = Convert.FromBase64String(soyad);
                entity.SOYADI = ASCIIEncoding.ASCII.GetString(soyadcozumdizi);

                string mail = dr[3].ToString();
                byte[] mailcozumdizi = Convert.FromBase64String(mail);
                entity.EMAIL = ASCIIEncoding.ASCII.GetString(mailcozumdizi);

                string sifre = dr[4].ToString();
                byte[] sifrecozumdizi = Convert.FromBase64String(sifre);
                entity.SIFRESI = ASCIIEncoding.ASCII.GetString(sifrecozumdizi);

                string hesapno = dr[5].ToString();
                byte[] hesapnocozumdizi = Convert.FromBase64String(hesapno);
                entity.HESAP_NO = ASCIIEncoding.ASCII.GetString(hesapnocozumdizi);
                list.Add(entity);
            }
            bgl.Close();
            dataGridView2.DataSource = list;
            dataGridView2.Columns["ID1"].Visible = false;
            dataGridView2.RowHeadersVisible = false;
        }

       

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             idno = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtad.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            txthesapno.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtmail.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtsifre.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtsoyad.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idno = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
      
            string ad = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            byte[] adcozumdizi = Convert.FromBase64String(ad);
           string adcozum = ASCIIEncoding.ASCII.GetString(adcozumdizi);
            txtad.Text = adcozum;

            string soyad =dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            byte[] soyadcozumdizi = Convert.FromBase64String(soyad);
            string soyadcozum = ASCIIEncoding.ASCII.GetString(soyadcozumdizi);
            txtsoyad.Text = soyadcozum;

            string mail = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            byte[] mailcozumdizi = Convert.FromBase64String(mail);
            string emailcozum = ASCIIEncoding.ASCII.GetString(mailcozumdizi);
            txtmail.Text = emailcozum;

            string sifre = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            byte[] sifrecozumdizi = Convert.FromBase64String(sifre);
            string sifrecozum= ASCIIEncoding.ASCII.GetString(sifrecozumdizi);
            txtsifre.Text = sifrecozum;

            string hesapno = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            byte[] hesapnocozumdizi = Convert.FromBase64String(hesapno);
            string hesapnocozum = ASCIIEncoding.ASCII.GetString(hesapnocozumdizi);
            txthesapno.Text = hesapnocozum;


        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("delete from TBLVERILER where ID="+idno, bgl);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("VERI SILINDI");
            listele();
            temizle();
            SifreCozme();   }
        private void btnguncelle_Click(object sender, EventArgs e)
        {
                    if (txtad.Text != "" && txthesapno.Text != "" && txtmail.Text != "" && txtsifre.Text != "" && txtsoyad.Text != "")
             {
                string ad = txtad.Text;
                byte[] addizi = ASCIIEncoding.ASCII.GetBytes(ad);
                string adsifre = Convert.ToBase64String(addizi);

                string soyad = txtsoyad.Text;
                byte[] soyaddizi = ASCIIEncoding.ASCII.GetBytes(soyad);
                string soyadsifre = Convert.ToBase64String(soyaddizi);

                string mail = txtmail.Text;
                byte[] maildizi = ASCIIEncoding.ASCII.GetBytes(mail);
                string mailsifre = Convert.ToBase64String(maildizi);

                string sifre = txtsifre.Text;
                byte[] sifredizi = ASCIIEncoding.ASCII.GetBytes(sifre);
                string sifresifre = Convert.ToBase64String(sifredizi);

                string hesapno = txthesapno.Text;
                byte[] hesapnodizi = ASCIIEncoding.ASCII.GetBytes(hesapno);
                string hesapnosifre = Convert.ToBase64String(hesapnodizi);
                bgl.Open();
                SqlCommand komut = new SqlCommand("update  TBLVERILER set AD=@p1,SOYAD=@p2,MAIL=@p3,SIFRE=@p4,HESAPNO=@p5 where ID=@P6", bgl);
                komut.Parameters.AddWithValue("@p1", adsifre);
                komut.Parameters.AddWithValue("@p2", soyadsifre);
                komut.Parameters.AddWithValue("@p3", mailsifre);
                komut.Parameters.AddWithValue("@p4", sifresifre);
                komut.Parameters.AddWithValue("@p5", hesapnosifre);
                komut.Parameters.AddWithValue("@p6", idno);
                komut.ExecuteNonQuery();
                MessageBox.Show("VERİ GÜNCELLENDI.");
                temizle(); bgl.Close();
                listele();
                SifreCozme();
            }
            else
            {
                MessageBox.Show("LÜTFEN BİLGİLERİ EKSİKSİZ GİRİNİZ.");
            }

        }
    }
}
