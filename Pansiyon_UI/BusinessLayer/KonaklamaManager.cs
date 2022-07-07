using Pansiyon_UI.Dal;
using Pansiyon_UI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pansiyon_UI.BusinessLayer
{
    public class KonaklamaManager
    {
        KonaklamalarDal _konaklamaDal = new KonaklamalarDal();
        public void Ekle(Konaklamalar konaklama)
        {
            _konaklamaDal.Ekle(konaklama);
        }
        public void Sil(Konaklamalar konaklama)
        {
            _konaklamaDal.Sil(konaklama);
        }
        public void Guncelle(Konaklamalar konaklama)
        {
            _konaklamaDal.Guncelle(konaklama);
        }
        public List<Konaklamalar> Listele()
        {
            return _konaklamaDal.Listele();
        }
    }
}
