using System.Collections.Generic;
using System.Linq;
using Core;
using Data.Dtos;
using Data.Entities;
using Data.Models;

namespace Data.Access
{
    public class EFUrunDal : EfEntityRepositoryBase<Urun, MezatContext>
    {
     /*   public UrunResimModel AddUrun(UrunResimModel urunResimModel)
        {
            var u = Add(urunResimModel.Urun);
            urunResimModel.Urun = u;
            var urunResimDal = new EFUrunResimDal();
            for (var i =0;i<urunResimModel.Resimler.Count;i++)
            {
                var resim = urunResimModel.Resimler[i];
                urunResimModel.Resimler[i] = urunResimDal.AddResim(u.UrunID, resim.Base64);
            }
            return urunResimModel;
        }*/
        
        public List<Urun> GetAllbyKullanici(int kid)
        {
            using (MezatContext db = new MezatContext())
            {
                return db.Urun.Where(x => x.KullaniciID == kid && !x.UrunDurum).ToList();

            }
        }

        public List<Urun> GetAllbyKullaniciMuzayedesiz(int kid)
        {
            using (MezatContext db = new MezatContext())
            {
                var urunler = GetAllbyKullanici(kid);
                List<Urun> uruns = new List<Urun>();
                foreach (var urun in urunler)
                {
                    if (db.MuzayedeUrunleri.FirstOrDefault(x => x.UrunID == urun.UrunID) == null)
                    {
                        uruns.Add(urun);
                    }
                }

                return uruns;
            }
        }


    }
}