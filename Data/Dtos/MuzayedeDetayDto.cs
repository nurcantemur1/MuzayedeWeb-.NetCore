using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Dtos
{
    public class MuzayedeDetayDto
    {
        public Muzayede muzayede { get; set; }
        public List<MUrunDto> murunler { get; set; }
       

    }
}

