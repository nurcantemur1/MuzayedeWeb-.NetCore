using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Access;
using Data.Entities;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class SharedController : Controller
    {
        private SecurityController _securityController;
        IHttpContextAccessor accessor;
        EFKullaniciDal kullaniciDal = new EFKullaniciDal();
        public SharedController( IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
            _securityController = new SecurityController(accessor);
        }
        public IActionResult _Head()
        {
            var id = accessor.HttpContext.User.Identity.Name;
            
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.kullanici = kullaniciDal.Get(x => x.KullaniciID == int.Parse(id));
            }
            return PartialView();
        }
        
    }
}
