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
    public partial class FormOdalar : Form
    {
        public FormOdalar()
        {
            InitializeComponent();
        }
        OdaManager _odaManager = new OdaManager();
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxOdaId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tbxOdaNo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            tbxOdaFiyatı.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cbxOdaDurumu.Checked =Convert.ToBoolean( dataGridView1.CurrentRow.Cells[3].Value.ToString());
        }

        private void FormOdalar_Load(object sender, EventArgs e)
        {
            OdaListele();
        }
        private void OdaListele()
        {
            dataGridView1.DataSource = _odaManager.Listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Odalar oda = new Odalar()
            {
                OdaNo = tbxOdaNo.Text,
                Fiyat=Convert.ToDecimal(tbxOdaFiyatı.Text),
                MusaitMi=cbxOdaDurumu.Checked
            
            };


            _odaManager.Ekle(oda);
            OdaListele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Odalar oda = new Odalar()
            {
                ID=Convert.ToInt32(tbxOdaId.Text),
                OdaNo = tbxOdaNo.Text,
                Fiyat = Convert.ToDecimal(tbxOdaFiyatı.Text),
                MusaitMi = cbxOdaDurumu.Checked

            };

            _odaManager.Guncelle(oda);
            OdaListele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Odalar oda = new Odalar()
            {
                ID = Convert.ToInt32(tbxOdaId.Text),
                OdaNo = tbxOdaNo.Text,
                Fiyat = Convert.ToDecimal(tbxOdaFiyatı.Text),
                MusaitMi = cbxOdaDurumu.Checked

            };

            _odaManager.Sil(oda);
            OdaListele();

        }
    }
}
