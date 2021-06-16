using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Data.Access
{
    public class EFUrunResimDal : EfEntityRepositoryBase<UrunResim, MezatContext>
    {
       
        
        public Resim AddResim(int UrunID, string base64)
        {
            using (MezatContext db = new MezatContext())
            {
                Resim v = db.Resim.Add(new Resim
                {
                    Base64 = base64
                }).Entity;
                db.SaveChanges();
                UrunResim y = new UrunResim();
                y.UrunID = UrunID;
                y.ResimID = v.ResimID;
                db.UrunResim.Add(y);
                db.SaveChanges();
                return v;
            }
        }

        public List<Resim> GetUrunResim(int UrunID)
        {
            using (MezatContext db = new MezatContext())
            {
                List<UrunResim> list = db.UrunResim.Where(x => x.UrunID == UrunID).ToList();
                List<Resim> list1 = new List<Resim>();
                foreach (var item in list)
                {
                    var r = db.Resim.Find(item.ResimID);
                    list1.Add(r);
                }

                
                return list1;
            }

        }
    }

}
