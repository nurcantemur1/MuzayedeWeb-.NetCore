using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Data.Entities
{
    public class Kullanici 
    {
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string KullaniciSoyadi { get; set; }
        public string KullaniciAdres { get; set; }
        public string Kullanicimail { get; set; }
        public string KullaniciTelefon { get; set; }
        public string Sifre { get; set; }
    }
}
