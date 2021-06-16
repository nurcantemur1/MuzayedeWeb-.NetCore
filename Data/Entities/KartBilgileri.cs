using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class KartBilgileri
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public String HesapNo { get; set; }
        public String KartNo { get; set; }
        public decimal Bakiye { get; set; }
        public bool Varsayilan { get; set; }

    }
}
