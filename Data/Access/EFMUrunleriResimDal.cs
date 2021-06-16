using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Data.Entities;

namespace Data.Access
{
    public class EFMUrunleriResimDal : EfEntityRepositoryBase<MUrunleriResim, MezatContext>
    {
        public List<Resim> GetMUrunResim(int UrunID)
        {
            using (MezatContext db = new MezatContext())
            {
                int murunId = db.MuzayedeUrunleri.FirstOrDefault(x=> x.UrunID == UrunID).ID;
                List<MUrunleriResim> list = db.MUrunleriResim.Where(x => x.MUrunID == murunId).ToList();
                List<Resim> list1 = new List<Resim>();
                foreach (var item in list)
                {
                    var r = db.Resim.Find(item.ResimID);
                    list1.Add(r);
                }

                return list1;
            }
            
        }
        public Resim AddResim(int UrunID, string base64)
        {
            using (MezatContext db = new MezatContext())
            {
                Resim v = db.Resim.Add(new Resim
                {
                    Base64 = base64
                }).Entity;
                db.SaveChanges();
                MUrunleriResim y = new MUrunleriResim();
                y.MUrunID = db.MuzayedeUrunleri.FirstOrDefault(x=> x.UrunID == UrunID).ID;
                y.ResimID = v.ResimID;
                db.MUrunleriResim.Add(y);
                db.SaveChanges();
                return v;
            }
        }

    }
}
