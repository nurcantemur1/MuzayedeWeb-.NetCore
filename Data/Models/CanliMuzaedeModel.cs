using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Models
{
    public class CanliMuzaedeModel
    {
        public Muzayede muzayede { get; set; }

        public MurunleriModel murun { get; set; }
    }
}
