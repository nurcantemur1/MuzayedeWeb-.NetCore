using Data.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class PeyModel
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public string UrunAciklamasi { get; set; }
        public decimal UrunFiyat { get; set; }
        public int MuzayedeID { get; set; }
        public string MuzayedeAdi { get; set; }
        public int PeyID { get; set; }
        public decimal Pey { get; set; }
        public String PeyZaman { get; set; }
        public int KullaniciID { get; set; }
        public string KullaniciAdiSoyadi { get; set; }
        public string Base64 { get; set; }

    }
    
}
