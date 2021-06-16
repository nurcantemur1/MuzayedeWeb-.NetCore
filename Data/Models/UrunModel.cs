using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Models
{
    public class UrunModel
    {
        public Urun urun { get; set; }
        public Kullanici kullanici { get; set; }
        public Kategori kategori { get; set; }
    }
}
