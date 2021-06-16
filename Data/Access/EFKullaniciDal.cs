using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dtos;
using Data.Entities;

namespace Data.Access
{
    public class EFKullaniciDal : EfEntityRepositoryBase<Kullanici, MezatContext>
    {
        public bool MailKontrol(string mail)
        {
            using (MezatContext db = new MezatContext())
            {
                return db.Kullanici.FirstOrDefault(x => x.Kullanicimail.Equals(mail)) == null;
            }
        }
        public Kullanici GirisKontrol(string mail, string sifre)
        {
            using (MezatContext db = new MezatContext())
            {
                return db.Kullanici.FirstOrDefault(x => x.Kullanicimail.Equals(mail) && x.Sifre.Equals(sifre));
            }
        }
    }
}
