using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Http;

namespace WebApp.Models
{
    public class UrunModel
    {
        public Urun urun { get; set; }
        public List<Resim> resim { get; set; }
    }
}
