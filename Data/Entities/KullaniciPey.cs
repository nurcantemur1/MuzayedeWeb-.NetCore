using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class KullaniciPey
    {
        [Key]
        public int PeyID { get; set; }
        public decimal Pey { get; set; }
        public System.DateTime PeyZaman { get; set; }
        public int KullaniciID { get; set; }  
        public int MurunID { get; set; }
    }
}
