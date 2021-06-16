using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Access;
using Data.Entities;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private EFKullaniciDal kullaniciDal = new EFKullaniciDal();
        //update eksik
        [HttpPost("add")]
        public IActionResult Add(Kullanici kullanici)
        {
            kullanici = kullaniciDal.Add(kullanici);
            if (kullanici != null)
            {
                return Ok(kullanici);
            }
            return BadRequest();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(kullaniciDal.GetAll());
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(kullaniciDal.Get(kullanici => kullanici.KullaniciID == id));
            
        }
        [HttpPost("update")]
        public IActionResult Update(Kullanici kullanici)
        {
            return Ok(kullaniciDal.Update(kullanici));
        }

        [HttpGet("MailKontrol/{mail}")]
        public IActionResult MailKontrol(string mail)
        {
            return Ok(kullaniciDal.MailKontrol(mail));
        }
        
        [HttpGet("GirisKontrol/{mail}/{sifre}")]
        public IActionResult GirisKontrol(string mail, string sifre)
        {
            return Ok(kullaniciDal.GirisKontrol(mail,sifre));
        }
    }
}
