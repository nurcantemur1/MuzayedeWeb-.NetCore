using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class MUrunleriResim 
    {
        [Key]
        public int MurunResimID { get; set; }
        public int MUrunID { get; set; }
        public int ResimID { get; set; }
    }
}
