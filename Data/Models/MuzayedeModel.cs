using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Models
{
    public class MuzayedeModel
    {

        public Muzayede muzayede { get; set; }
        public List<MurunleriModel> muzayedeUrunleri { get; set; }
    }
}
