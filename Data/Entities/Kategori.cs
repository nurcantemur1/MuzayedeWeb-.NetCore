using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Data.Entities
{
    public class Kategori 
    {
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }
        public bool KategoriDurum { get; set; }
    }
}
