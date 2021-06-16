using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;

namespace WebApp.Models
{
    public class UrunListModel
    {
       public Urun urun { get; set; }
        public bool check { get; set; }
        public string Base64 { get; set; }
    }
}
