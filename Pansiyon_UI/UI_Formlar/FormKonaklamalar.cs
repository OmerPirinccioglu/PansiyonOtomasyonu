using Pansiyon_UI.BusinessLayer;
using Pansiyon_UI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pansiyon_UI.UI_Formlar
{
    public partial class FormKonaklamalar : Form
    {
        public int MusteriId { get; set; }
        public string MusteriAdiSoyadi { get; set; }
        public FormKonaklamalar()
        {
            InitializeComponent();
        }
        KonaklamaManager _konaklama = new KonaklamaManager();
        private void FormKonaklamalar_Load(object sender, EventArgs e)
        {
            tbxMusteriAdiSoyadi.Text = MusteriAdiSoyadi;
            GunSayisiHesapla();
            KonaklamaListele();
            OdaSec();
        }

        private void GunSayisiHesapla()
        {
            TimeSpan gunSayisi;
            gunSayisi = DateTime.Parse(dtpCikisTarihi.Text) - DateTime.Parse(dtpGirisTarihi.Text);
            tbxGunSayisi.Text = gunSayisi.TotalDays.ToString();
        }
        private void KonaklamaListele()
        {
            dataGridView1.DataSource = _konaklama.Listele().Where(k => k.AktifMi == true&&k.MusteriID==MusteriId).ToList();
        }

        private void dtpGirisTarihi_ValueChanged(object sender, EventArgs e)
        {
            GunSayisiHesapla();
            ToplamFiyatHesapla();
        }

        private void dtpCikisTarihi_ValueChanged(object sender, EventArgs e)
        {
            GunSayisiHesapla();
            ToplamFiyatHesapla();
        }

        private void OdaSec()
        {
            OdaManager odaManager = new OdaManager();
            int pozisyonX = 50;
            int pozisyonY = 50;
            int sutun = 1;
            for (int i = 0; i < odaManager.Listele().Count; i++)
            {
                Button button = new Button();
                button.Location = new Point(pozisyonX, pozisyonY);
                button.Height = 50;
                button.Width = 50;
                button.Text = odaManager.Listele()[i].OdaNo.ToString();
                button.Name = odaManager.Listele()[i].ID.ToString();
                button.Click += new EventHandler(SeciliButon);
                grpboxOdalar.Controls.Add(button);
                if (!odaManager.Listele()[i].MusaitMi)
                {
                    button.BackColor = Color.Red;
                    button.Enabled = false;
                }
                else
                {
                    button.BackColor = Color.Green;
                }
                if (sutun < 5)
                {
                    pozisyonX += 80;
                    sutun++;
                }
                else
                {
                    sutun = 1;
                    pozisyonX = 50;
                    pozisyonY += 80;
                }
            }



        }

        private void SeciliButon(object sender, EventArgs eventArgs)
        {
            Button dinamikButon = (sender as Button);
            tbxOdaNo.Text = dinamikButon.Text;
            tbxOdaId.Text = dinamikButon.Name;
            OdaManager odaManager = new OdaManager();
            int id = Convert.ToInt32(tbxOdaId.Text);
            Odalar oda = odaManager.OdaGetir(id);
            tbxOdaFiyatı.Text = oda.Fiyat.ToString();
            ToplamFiyatHesapla();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (tbxOdaId.Text != "" && tbxToplamFiyat.Text != "" && cboxAktifMi.Checked)
            {
                Konaklamalar konaklama = new Konaklamalar()
                {
                    MusteriID = MusteriId,
                    OdaID = Convert.ToInt32(tbxOdaId.Text),
                    GirisTarihi = dtpGirisTarihi.Value,
                    CikisTarihi = dtpCikisTarihi.Value,
                    AktifMi = cboxAktifMi.Checked,
                    ToplamFiyat = Convert.ToDecimal(tbxToplamFiyat.Text)
                };
                _konaklama.Ekle(konaklama);
                OdaManager odaManager = new OdaManager();
                Odalar oda = new Odalar();
                oda = odaManager.Listele().Where(o => o.OdaNo == tbxOdaNo.Text).FirstOrDefault();
                odaManager.OdaGuncelleKonaklamaGiris(oda);

                KonaklamaListele();
                grpboxOdalar.Controls.Clear();
                OdaSec();
            }
            else
            {
                MessageBox.Show("Lütfen önce bir oda seçin ve boş alanları doldurun(Tarih, Fiyat ve Konaklama Durumu)");
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (tbxOdaId.Text !=""&&tbxToplamFiyat.Text!=""&&tbxKonaklamaId.Text!="")
            {
                Konaklamalar konaklama = new Konaklamalar() 
                {
                    ID= Convert.ToInt32(tbxKonaklamaId.Text),
                    MusteriID=MusteriId,
                    OdaID = Convert.ToInt32(tbxOdaId.Text),
                    GirisTarihi=dtpGirisTarihi.Value,
                    CikisTarihi=dtpCikisTarihi.Value,
                    AktifMi=cboxAktifMi.Checked,
                    ToplamFiyat=Convert.ToDecimal(tbxToplamFiyat.Text)
                };

                _konaklama.Guncelle(konaklama);

                if (!konaklama.AktifMi)
                {
                    OdaManager odaManager = new OdaManager();
                    Odalar oda = new Odalar();
                    oda = odaManager.Listele().Where(o => o.ID == Convert.ToInt32(tbxOdaId.Text)).FirstOrDefault();
                    odaManager.OdaGuncelleKonaklamaCikis(oda);

                }
                KonaklamaListele();
                grpboxOdalar.Controls.Clear();
                OdaSec();
            }
            else
            {
                MessageBox.Show("Lütfen bir kayıt seçin");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxKonaklamaId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tbxOdaId.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dtpGirisTarihi.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            dtpCikisTarihi.Value= Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value.ToString());
            tbxToplamFiyat.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            cboxAktifMi.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[6].Value);
            
        }

        private void ToplamFiyatHesapla()
        {
            tbxToplamFiyat.Text = (Convert.ToDecimal(tbxOdaFiyatı.Text) * Convert.ToInt32(tbxGunSayisi.Text)).ToString();
        }
    }
}
