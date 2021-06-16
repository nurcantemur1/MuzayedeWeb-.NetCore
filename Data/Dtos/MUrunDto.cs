using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class MUrunDto
    {
        public int mUrunID { get; set; }
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public int KullaniciID { get; set; }
        public int UrunAdet { get; set; }
        public decimal UrunFiyat { get; set; }
        public string UrunAciklamasi { get; set; }
        public bool UrunDurum { get; set; }
        public string Base64 { get; set; }
    }
}
