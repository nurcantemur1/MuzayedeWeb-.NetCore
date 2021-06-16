using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Data.Dtos;
using Data.Entities;

namespace Data.Access
{
    public class EFMuzayedeDal : EfEntityRepositoryBase<Muzayede, MezatContext>
    {
        public List<Muzayede> GetAllKullaniciMuzayedeleri(int kId)
        {
            using (MezatContext db = new MezatContext())
            {
                return db.Muzayede.Where(x => x.KullaniciID == kId).ToList();
                
            }
        }

        public Muzayede AddMuzayede(Muzayede muzayede)
        {
            Muzayede m = new Muzayede
            {
                KullaniciID = muzayede.KullaniciID,
                MuzayedeAdi = muzayede.MuzayedeAdi,
                Izlenme = 0,
                MTarih = DateTime.Now,
                Sure = TimeSpan.FromHours(10),
                Date = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")

            };
                m = Add(m);
                return m;
            
        }
        public Muzayede UpdateMuzayede(Muzayede muzayede)
        {
            Muzayede m = new Muzayede
            {
                KullaniciID = muzayede.KullaniciID,
                MuzayedeAdi = muzayede.MuzayedeAdi,
                Izlenme = 0,
                MTarih = DateTime.Now,
                Sure = TimeSpan.FromHours(10),
                Date = muzayede.Date,
                MuzayedeID = muzayede.MuzayedeID

            };
            m = Update(m);
            return m;

        }
        
        
    }
}