using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Muzayede
    {
        public int MuzayedeID { get; set; }
        public System.TimeSpan Sure { get; set; }
        public System.DateTime MTarih { get; set; }
        public string MuzayedeAdi { get; set; }
        public int KullaniciID { get; set; }
        public int Izlenme { get; set; }
        public String Date { get; set; }
    }
}
