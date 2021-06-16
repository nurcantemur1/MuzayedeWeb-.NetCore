using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data
{
    public class MezatContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=MezatDB;User Id=sa;Password=1; MultipleActiveResultSets=True"); 
        }

        public DbSet<Muzayede> Muzayede { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<MuzayedeUrunleri> MuzayedeUrunleri { get; set; }
        public DbSet<Urun> Urun { get; set; }
        public DbSet<UrunResim> UrunResim { get; set; }
        public DbSet<Resim> Resim { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Siparis> Siparis { get; set; }
        public DbSet<KullaniciPey> KullaniciPey { get; set; }
        public DbSet<SiparisDetay> SiparisDetay { get; set; }
        public DbSet<KartBilgileri> KartBilgileri { get; set; }
        public DbSet<MUrunleriResim> MUrunleriResim { get; set; }









    }
}
