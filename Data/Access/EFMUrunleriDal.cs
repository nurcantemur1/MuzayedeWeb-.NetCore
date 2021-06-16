using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Data.Dtos;
using Data.Entities;

namespace Data.Access
{
    public class EFMUrunleriDal : EfEntityRepositoryBase<MuzayedeUrunleri, MezatContext>
    {
        
        public int GetMurunId(int urunid, int muzayedeid)
        {
            using (MezatContext db = new MezatContext())
            {
                var murun = db.MuzayedeUrunleri.FirstOrDefault(x => x.UrunID == urunid &&  x.MuzayedeID == muzayedeid);
                if (murun != null) return murun.ID;
                return 0;
            }
        }
        public int GetMuzayedeId(int urunid)
        {
            using (MezatContext db = new MezatContext())
            {
                var muzayede = db.MuzayedeUrunleri.FirstOrDefault(x => x.UrunID == urunid );
                if (muzayede != null) return muzayede.MuzayedeID;
                return 0;
            }
        }
        public MuzayedeDetayDto GetMuzayedeDetay(int muzayedeId)
        {
            using (MezatContext db = new MezatContext())
            {
                var dto = new MuzayedeDetayDto();
                dto.muzayede = db.Muzayede.Find(muzayedeId);
                dto.murunler = new List<MUrunDto>();
                var list = db.MuzayedeUrunleri.Where(x => x.MuzayedeID == muzayedeId).ToList();
                foreach (var murun in list)
                {
                    Urun urun = db.Urun.Find(murun.UrunID);
                    var resimler = db.UrunResim.Where(x => x.UrunID == urun.UrunID).ToList();
                    string base64 = "";
                    if (resimler.Count > 0)
                    {
                        var resimId = resimler[0].ResimID;
                        base64 = db.Resim.Find(resimId).Base64;
                    }
                    dto.murunler.Add(new MUrunDto
                    {
                        mUrunID = murun.ID,
                        KullaniciID = urun.KullaniciID,
                        UrunAciklamasi = urun.UrunAciklamasi,
                        UrunAdet = urun.UrunAdet,
                        UrunAdi = urun.UrunAdi,
                        UrunDurum = urun.UrunDurum,
                        UrunFiyat = urun.UrunFiyat,
                        UrunID = urun.UrunID,
                        Base64 = base64
                    }); 
                }
                return dto;
            }
        }

    }
}