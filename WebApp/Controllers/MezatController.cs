using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Access;
using Data.Dtos;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class MezatController : Controller
    {
        private EFMuzayedeDal _efMuzayedeDal = new EFMuzayedeDal();
        private EFMUrunleriDal _efmUrunleriDal = new EFMUrunleriDal();
        private EFKullaniciPeyDal _efKullaniciPeyDal = new EFKullaniciPeyDal();
        public IActionResult Kontrol(int muzayedeId)
        {
           
            var date = _efMuzayedeDal.Get(x=> x.MuzayedeID == muzayedeId).Date;
            if (DateTime.Compare(DateTime.Now, DateTime.Parse(date)) > 0)
            {
                return RedirectToAction("MuzayedeDetay", new { muzayedeId });
            }
            return RedirectToAction("CanliMuzayede", new { muzayedeId });
        }

        public IActionResult MuzayedeDetay(int muzayedeId)
        {
            var model = _efmUrunleriDal.GetMuzayedeDetay(muzayedeId);
            return View(model);
        }
        public IActionResult CanliMuzayede(int muzayedeId=1)
        {
            var model = _efmUrunleriDal.GetMuzayedeDetay(muzayedeId);
            var list = model.murunler;
            ViewBag.json = JsonConvert.SerializeObject(list);
            return View();
        }

        [HttpPost]
        public string UpdatePey(int murunid)
        {
            var kpey = _efKullaniciPeyDal.UpdateSonPey(4, murunid);
            return JsonConvert.SerializeObject(kpey);
        }
        
        [HttpGet]
        public string GetSonPey(int murunid)
        {
            var kpey = _efKullaniciPeyDal.GetSonpey(murunid);
            return JsonConvert.SerializeObject(kpey);
        }
        [HttpGet]
        public string GetResimler(int urunid)
        {
            var resimler = new EFUrunResimDal().GetUrunResim(urunid);
            return JsonConvert.SerializeObject(resimler);
        }
    }
}
