using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Data.Access;
using Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class KullaniciController : Controller
    {
        private SecurityController _securityController ;
        private EFKullaniciDal _efKullaniciDal = new EFKullaniciDal();
        private EFUrunDal _efUrunDal = new EFUrunDal();
        private EFMuzayedeDal _efMuzayedeDal = new EFMuzayedeDal();
        private EFKullaniciPeyDal _efKullaniciPeyDal = new EFKullaniciPeyDal();
        private EFKartBilgileriDal _efKartBilgileriDal = new EFKartBilgileriDal();
        private EFMUrunleriDal _efmUrunleriDal = new EFMUrunleriDal();
        private EFUrunResimDal _efUrunResimDal = new EFUrunResimDal();

        private readonly IWebHostEnvironment webHostEnvironment;

        public KullaniciController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            this._securityController = new SecurityController(httpContextAccessor);
        }
        public IActionResult KullaniciBilgileri()
        {
            var model = _efKullaniciDal.Get(x=> x.KullaniciID == _securityController.KullaniciId());

            return View(model);
        }
        [HttpPost]
        public IActionResult Kaydet(Kullanici kullanici)
        {
            kullanici.KullaniciID = _securityController.KullaniciId();
            _efKullaniciDal.Update(kullanici);
            return Redirect("KullaniciBilgileri");
        }
        public IActionResult Urunlerim()
        {
           
            int id = _securityController.KullaniciId();
            var list = _efUrunDal.GetAllbyKullanici(id);
            List<UrunModel> urunModels = new List<UrunModel>();
            foreach (var item in list)
            {
                UrunModel urunModel = new UrunModel();
                urunModel.urun = item;
                urunModel.resim = _efUrunResimDal.GetUrunResim(urunModel.urun.UrunID);
                urunModels.Add(urunModel);
            }
            return View(urunModels);
        }
        public IActionResult UrunEkle(int? muzayedeId)
        {
            if (muzayedeId == null)
            {
                ViewBag.muzayedeId = 0;
            }
            else
            {
                ViewBag.muzayedeId = muzayedeId;
            }
            return View(new Urun());
        }

     
        [HttpPost]
         public async Task<IActionResult> UrunEkle(Urun urun,int muzayedeId, List<IFormFile> dosya)
         {
              urun.KullaniciID = _securityController.KullaniciId();
              var id =_efUrunDal.Add(urun).UrunID;

              List<String> list = new List<string>();
                
                foreach (var formFile in dosya)
                {
                    if (formFile.Length > 0)
                    {
                        string imageExtension = Path.GetExtension(formFile.FileName);
                        string imageName = Guid.NewGuid() + imageExtension;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/resimler/{imageName}");
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                            stream.Close();
                            string path = webHostEnvironment.WebRootPath + "/resimler/" + imageName;
                            byte[] b = System.IO.File.ReadAllBytes(path);
                            var base64 = "data:image/png;base64," + Convert.ToBase64String(b);
                            list.Add(base64);
                        }
                    }
                }
                foreach (var resim in list)
                {
                    _efUrunResimDal.AddResim(id, resim);
                }
            if (muzayedeId == 0) return RedirectToAction("Urunlerim", "Kullanici");

             var murun = new MuzayedeUrunleri();
             murun.UrunID = urun.UrunID;
             murun.MuzayedeID = muzayedeId;
             _efmUrunleriDal.Add(murun);
            return RedirectToAction("MuzayedeDetay", new { muzayedeId = murun.MuzayedeID });

         }
         public IActionResult UrunDetay(int id)
         {
             var model = _efUrunDal.Get(x=> x.UrunID == id);
            UrunModel urunModel = new UrunModel();
            urunModel.urun = model;
            urunModel.resim = _efUrunResimDal.GetUrunResim(id);
             return View(urunModel);
         }

         [HttpPost]
         public IActionResult UrunDetay(Urun urun)
         { 
            _efUrunDal.Update(urun);
       
             return RedirectToAction("Urunlerim", "Kullanici");
         }
        public IActionResult UrunSil(int id)
        {
            var urun = _efUrunDal.Get(x => x.UrunID == id);
            var resim = _efUrunResimDal.Get(x=> x.UrunID == id);
            _efUrunResimDal.Delete(resim);
            _efUrunDal.Delete(urun);
            return RedirectToAction("Urunlerim", "Kullanici");
        }
        public IActionResult Muzayedelerim()
        {
            int id = _securityController.KullaniciId();
            var list = _efMuzayedeDal.GetAllKullaniciMuzayedeleri(id);
            return View(list);
        }

        public IActionResult MuzayedeDetay(int muzayedeId)
        {
            var model = _efmUrunleriDal.GetMuzayedeDetay(muzayedeId);
            
            ViewBag.muzayedeId = muzayedeId;
            return View(model);
        }
        public IActionResult MuzayedeSil(int muzayedeId)
        {
            var model = _efMuzayedeDal.Get(x=> x.MuzayedeID == muzayedeId);
            _efMuzayedeDal.Delete(model);
            
            ViewBag.muzayedeId = muzayedeId;
            return Redirect("Muzayedelerim");
        }
        public IActionResult MuzayedeEkle(string MuzayedeAdi)
        {
            if (!string.IsNullOrEmpty(MuzayedeAdi))
            {
                var muzayede = new Muzayede();
                muzayede.MuzayedeAdi = MuzayedeAdi;
                muzayede.KullaniciID = _securityController.KullaniciId();
                muzayede.MTarih = DateTime.Now;
                muzayede.Sure = TimeSpan.FromHours(10); 
                _efMuzayedeDal.AddMuzayede(muzayede);
                
                return RedirectToAction("Muzayedelerim", "Kullanici");
            }
            return RedirectToAction("Muzayedelerim", "Kullanici");
        }
        public IActionResult MurunSil(int murunId)
        {
            int muzayedeId= _efmUrunleriDal.Get(x => x.ID == murunId).MuzayedeID;
            _efmUrunleriDal.Delete(_efmUrunleriDal.Get(urunleri => urunleri.ID == murunId));
            return RedirectToAction("MuzayedeDetay", "Kullanici", new { muzayedeId });
        }
        public IActionResult MuzayedeKaydet()
        {
            return RedirectToAction("Muzayedelerim", "Kullanici");
        }
        public IActionResult UrunCheckList(int muzayedeId)
        {
            ViewBag.muzayedeId = muzayedeId;
            var list = _efUrunDal.GetAllbyKullaniciMuzayedesiz(_securityController.KullaniciId());
            var modellist = new List<UrunListModel>();
            foreach (var urun in list)
            {
                if (!_efmUrunleriDal.Equals(urun.UrunID))
                {
                    var model = new UrunListModel();
                    model.urun = urun;
                    var resimler = _efUrunResimDal.GetUrunResim(urun.UrunID);
                    if(resimler.Count > 0)
                    {
                         model.Base64 = _efUrunResimDal.GetUrunResim(urun.UrunID)[0].Base64;
                    }
                    else
                    {
                        model.Base64 = "";
                    }
                    model.check = false;
                    modellist.Add(model);
                    
                }
            }
            
            return View(modellist);
        }
        [HttpPost]
        public IActionResult UrunCheckList(List<UrunListModel> modellist,int muzayedeId)
        {
          
            if (modellist != null)
            {
               
                foreach (var model in modellist)
                { 
                    if (model.check)
                    {
                        var murun = new MuzayedeUrunleri();
                        murun.UrunID = model.urun.UrunID;
                        murun.MuzayedeID = muzayedeId;
                        _efmUrunleriDal.Add(murun);
                    }
                }
            }

            return RedirectToAction("MuzayedeDetay", "Kullanici",new {muzayedeId} );
          
        }

        public IActionResult PeyVerilenler()
        {
            var model = _efKullaniciPeyDal.GetAllbyKullanici(_securityController.KullaniciId());
            List<PeyModel> peyModellist = new List<PeyModel>();

            foreach (var item in model)
            {
                PeyModel peyModel = new PeyModel();
                peyModel.MuzayedeID = item.muzayede.MuzayedeID;
                peyModel.MuzayedeAdi = item.muzayede.MuzayedeAdi;
                peyModel.PeyID = item.PeyID;
                peyModel.Pey = item.Pey;
                peyModel.PeyZaman = item.PeyZaman;
                peyModel.UrunID = item.urun.UrunID;
                peyModel.UrunAdi = item.urun.UrunAdi;
                peyModel.UrunFiyat = item.urun.UrunFiyat;
                peyModel.UrunAciklamasi = item.urun.UrunAciklamasi +" "+ item.urun.UrunAdet+ "adet";
                var resimler = _efUrunResimDal.GetUrunResim(item.urun.UrunID);
                if (resimler.Count > 0)
                {
                peyModel.Base64 = _efUrunResimDal.GetUrunResim(item.urun.UrunID)[0].Base64;

                }
                else
                {
                    peyModel.Base64 = "";
                }
                peyModellist.Add(peyModel);
                
            }
            return View(peyModellist);
        }

        public IActionResult Siparislerim()
        {
            var model = _efKullaniciPeyDal.GetSiparisListbyKullanici(_securityController.KullaniciId());
            List<PeyModel> peyModellist = new List<PeyModel>();

            foreach (var item in model)
            {
                PeyModel peyModel = new PeyModel();
                peyModel.MuzayedeID = item.muzayede.MuzayedeID;
                peyModel.MuzayedeAdi = item.muzayede.MuzayedeAdi;
                peyModel.PeyID = item.PeyID;
                peyModel.Pey = item.Pey;
                peyModel.PeyZaman = item.PeyZaman;
                peyModel.UrunID = item.urun.UrunID;
                peyModel.UrunAdi = item.urun.UrunAdi;
                peyModel.UrunFiyat = item.urun.UrunFiyat;
                peyModel.UrunAciklamasi = item.urun.UrunAciklamasi + " " + item.urun.UrunAdet + "adet";
                var resimler = _efUrunResimDal.GetUrunResim(item.urun.UrunID);
                if (resimler.Count > 0)
                {
                    peyModel.Base64 = _efUrunResimDal.GetUrunResim(item.urun.UrunID)[0].Base64;

                }
                else
                {
                    peyModel.Base64 = "";
                }
                peyModellist.Add(peyModel);

            }
            return View(peyModellist);
        }

        public IActionResult KartBilgilerim()
        {
            var model = _efKartBilgileriDal.GetAllByKullaniciId(_securityController.KullaniciId());
            ViewBag.kullaniciId = _securityController.KullaniciId();
            return View(model);
        }
        public IActionResult KartEkle( int kullaniciId)
        {
            ViewBag.kullaniciId = kullaniciId;
            return View(new KartBilgileri());
        }
    
        [HttpPost]
        public IActionResult KartEkle(KartBilgileri kartBilgileri, int kullaniciId)
        {
            // var kart = new KartBilgileri();
            
            _efKartBilgileriDal.Add(kartBilgileri);
            return RedirectToAction("KartBilgilerim");
        }
        
        public IActionResult VarsayilanYap(int kartId)
        {
            //ViewBag.kartId = kartId;
            _efKartBilgileriDal.VarsayilanYap(kartId);
            return Redirect("KartBilgilerim");
        }
    }
}
