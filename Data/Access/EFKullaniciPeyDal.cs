using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Data.Dtos;
using Data.Entities;

namespace Data.Access
{
    public class EFKullaniciPeyDal : EfEntityRepositoryBase<KullaniciPey, MezatContext>
    {
       
        
        public List<KullaniciPeyDto> GetAllbyKullanici(int kullaniciId)
        {
            using (MezatContext db = new MezatContext())
            {
                var list = db.KullaniciPey.Where(x => x.KullaniciID == kullaniciId).ToList();
                var peyverilenler = new List<KullaniciPeyDto>();
                foreach (var item in list)
                {
                    var dto = new KullaniciPeyDto();
                    dto.PeyID = item.PeyID;
                    dto.KullaniciID = item.KullaniciID;
                    dto.urun = db.Urun.Find(db.MuzayedeUrunleri.Find(item.MurunID).UrunID);
                    dto.muzayede = db.Muzayede.Find(db.MuzayedeUrunleri.Find(item.MurunID).MuzayedeID);
                    dto.Pey = item.Pey;
                    dto.PeyZaman = item.PeyZaman.ToString();
                    peyverilenler.Add(dto);
                }

                return peyverilenler;
            }
        }
        public KullaniciPeyDto GetSonpey(int murunid)
        {
            using (MezatContext db = new MezatContext())
            {
                
                var list = db.KullaniciPey.Where(x => x.MurunID == murunid).ToList();
                if (list.Count == 0) return null;
                var son = list[list.Count() - 1];
                return new KullaniciPeyDto
                {
                    KullaniciID = son.KullaniciID,
                    urun = db.Urun.Find(db.MuzayedeUrunleri.Find(son.MurunID).UrunID),
                    muzayede = db.Muzayede.Find(db.MuzayedeUrunleri.Find(son.MurunID).MuzayedeID),
                    PeyID = son.PeyID,
                    PeyZaman = son.PeyZaman.ToString(),
                    Pey = son.Pey
                };
            }
        }
        public KullaniciPeyDto UpdateSonPey(int kid, int murunid)
        {
            using (MezatContext db = new MezatContext())
            {
                var pey = GetSonpey(murunid);
                EFKartBilgileriDal kartBilgileriDal = new EFKartBilgileriDal();
                var yeniKullaniciKart = kartBilgileriDal.GetDefaultByKullaniciId(kid);

                
                if (pey == null)
                {
                    var fiyat = db.Urun.Find(db.MuzayedeUrunleri.Find(murunid).UrunID).UrunFiyat;
                    if (yeniKullaniciKart.Bakiye < fiyat)
                    {
                        return null;
                    }

                    yeniKullaniciKart.Bakiye -= fiyat;
                    kartBilgileriDal.Update(yeniKullaniciKart);
                    db.KullaniciPey.Add(new KullaniciPey
                    {
                        KullaniciID = kid,
                        Pey = fiyat,
                        MurunID = murunid,
                        PeyZaman = DateTime.Now,
                    });
                    db.SaveChanges();
                    return GetSonpey(murunid);
                }
                var eskiKullaniciKart = kartBilgileriDal.GetDefaultByKullaniciId(pey.KullaniciID);

                if (pey.KullaniciID != kid)
                {
                    var yeniTutar = pey.Pey + 1;
                    if (yeniKullaniciKart.Bakiye < yeniTutar)
                    {
                        return null;
                    }
                    eskiKullaniciKart.Bakiye += pey.Pey;
                    kartBilgileriDal.Update(eskiKullaniciKart);
                    
                    yeniKullaniciKart.Bakiye -= yeniTutar;
                    kartBilgileriDal.Update(yeniKullaniciKart);
                    
                    db.KullaniciPey.Add(new KullaniciPey
                    {
                        KullaniciID = kid,
                        Pey = yeniTutar,
                        MurunID = murunid,
                        PeyZaman = DateTime.Now,
                    });
                    db.SaveChanges();
                    return GetSonpey(murunid);
                }

                return null;

            }
        }
        public List<KullaniciPeyDto> GetSiparisListbyKullanici(int kullaniciId)
        {
            using (MezatContext db = new MezatContext())
            {
                var alinanlar = new List<KullaniciPeyDto>();
                var list = db.KullaniciPey.Where(x => x.KullaniciID == kullaniciId).ToList();
                var murunIdlist = list.Select(x => x.MurunID).Distinct().ToList();
                foreach (var murunId in murunIdlist)
                {
                    var dto = GetSonpey(murunId);
                    if (dto.KullaniciID == kullaniciId)
                    {
                        alinanlar.Add(dto);
                    }
                }
                return alinanlar;
            }
        }

    }
}