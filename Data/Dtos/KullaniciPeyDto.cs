using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Dtos
{
    public class KullaniciPeyDto
    {
        public int PeyID { get; set; }
        public Muzayede muzayede { get; set; }
        public Urun urun { get; set; }
        public decimal Pey { get; set; }
        public String PeyZaman { get; set; }
        public int KullaniciID { get; set; }
       
    }
}
