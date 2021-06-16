using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Data.Entities;

namespace Data.Access
{
    public class EFKartBilgileriDal : EfEntityRepositoryBase<KartBilgileri, MezatContext>
    {

        public List<KartBilgileri> GetAllByKullaniciId(int KullaniciId)
        {
            using (MezatContext db = new MezatContext())
            {
                return db.KartBilgileri.Where(x => x.KullaniciId == KullaniciId).ToList();
            }
        }

        public KartBilgileri GetDefaultByKullaniciId(int KullaniciId)//varsayılan kart bilgisi
        {
            using (MezatContext db = new MezatContext())
            {
                return  db.KartBilgileri.FirstOrDefault(x => x.KullaniciId == KullaniciId && x.Varsayilan);
            }
        }

        public KartBilgileri VarsayilanYap(int id)
        {
            using (MezatContext db = new MezatContext())
            {
                var kart = db.KartBilgileri.Find(id);
                var list = db.KartBilgileri.Where(x => x.KullaniciId == kart.KullaniciId).ToList();
                foreach (var kartBilgileri in list)
                {
                    kartBilgileri.Varsayilan = false;
                    Update(kartBilgileri);
                }
                kart.Varsayilan = true;
                return Update(kart);
            }
        }
    }
}
