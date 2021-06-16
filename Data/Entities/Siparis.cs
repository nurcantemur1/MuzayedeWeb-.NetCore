using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Siparis
    {
        public int SiparisID { get; set; }
        public System.DateTime SiparisTarih { get; set; }
        public decimal SiparisTutari { get; set; }
        public bool SiparisDurum { get; set; }
        public int SiparisDetayID { get; set; }
        public int KullaniciID { get; set; }
    }
}
