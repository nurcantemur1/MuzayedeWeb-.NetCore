using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data;
using Data.Access;
using Data.Entities;
using WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using UrunModel = WebApp.Models.UrunModel;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EFMuzayedeDal _efMuzayedeDal = new EFMuzayedeDal();
        private EFKullaniciDal _efKullaniciDal = new EFKullaniciDal();
        private EFKategoriDal _efKategoriDal = new EFKategoriDal();
        private EFUrunDal _efUrunDal = new EFUrunDal();
        private EFUrunResimDal _efUrunResimDal = new EFUrunResimDal();
        IHttpContextAccessor accessor;
        private SecurityController _securityController;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            this.accessor = accessor;
            _securityController = new SecurityController(this.accessor);
        }

        public IActionResult Index()
        {
            
            var model = _efMuzayedeDal.GetAll();
            var sonuc = model.Cast<Muzayede>().ToArray();

            var liste = new List<object>();

            liste.Add(sonuc);


            //Jun 6, 2021 22:15:25     MMMM dd, yyyy HH:mm:ss
            /*  foreach (var muzayede in model)
              {
                  var tarih = muzayede.Date.Split(" ")[0];
                  var saat = muzayede.Date.Split(" ")[1];
                  muzayede.Date = new DateTimeOffset(DateTime.Parse(muzayede.Date)).ToUnixTimeMilliseconds().ToString();
              }
            */
            return View(sonuc);
          
        }

        public IActionResult Galeri()
        {
            ViewBag.Kategoriler = _efKategoriDal.GetAll();
            using (MezatContext db = new MezatContext())
            {
                var modellist = new List<UrunModel>();
                var urunlerlist = db.Urun.ToList();
                foreach (var urun in urunlerlist)
                {
                    var model = new UrunModel();
                    model.urun = urun;
                    model.resim = _efUrunResimDal.GetUrunResim(urun.UrunID);
                    modellist.Add(model);
                }
                return View(modellist);
            }
        }

        public IActionResult Contact()
        {
                ViewBag.Message = "İletişim";
                return View();
        }
        public IActionResult EkspertizFormu()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Kullanici kullanici)
        {
            var model = _efKullaniciDal.GirisKontrol(kullanici.Kullanicimail, kullanici.Sifre);

            if (model != null)
            {
                var claims = new[] { 
                    new Claim(ClaimTypes.Name, model.KullaniciID.ToString()),
                    new Claim(ClaimTypes.Role, "user")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
                accessor.HttpContext.User = new ClaimsPrincipal(identity);
               
                var id = accessor.HttpContext.User.Identity.Name;

                return RedirectToAction("Index", "Home");

            }
            return Redirect("Login");
        }
        public IActionResult Login()
        {
                if (!string.IsNullOrEmpty(accessor.HttpContext.User.Identity.Name))
                {
                    return RedirectToAction("index", "Home");
                }
                return View();
            }
        public IActionResult KayitOl()
        {
            return View(new Kullanici());
        }
        [HttpPost]
        public IActionResult KayitOl(Kullanici model)
        {
            if (_efKullaniciDal.GirisKontrol(model.Kullanicimail,model.Sifre) == null)
            {
                _efKullaniciDal.Add(
                    new Kullanici
                    {
                        Kullanicimail = model.Kullanicimail,
                        Sifre = model.Sifre,
                        KullaniciAdi = model.KullaniciAdi,
                        KullaniciAdres = "-",
                        KullaniciSoyadi = model.KullaniciSoyadi,
                        KullaniciTelefon = "-"
                    });
                return RedirectToAction("Login", "Home");
            }
            return View(model);

        }

        [HttpGet]
        public ActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            accessor.HttpContext.User = null;
            //   Session["KullaniciID"] = null;
            // Session.Abandon();//sonlandırmak için sessionları
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
