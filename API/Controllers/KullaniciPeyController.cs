using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Access;
using Data.Dtos;
using Data.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciPeyController : ControllerBase
    {
        private EFKullaniciPeyDal kullaniciPeyDal = new EFKullaniciPeyDal();
        [HttpPost("add")]
        public IActionResult Add(KullaniciPey dto)
        {
            dto = kullaniciPeyDal.Add(dto);
            return Ok(dto);
        }
        [HttpPost("sonpeyupdate/{kid}/{murunid}")]
        public IActionResult SonPeyUpdate(int kid, int murunid)
        {
            KullaniciPeyDto dto = kullaniciPeyDal.UpdateSonPey(kid, murunid);
            if(dto != null) return Ok(dto);
            return BadRequest("Eklenemedi");
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(kullaniciPeyDal.GetAll());
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(kullaniciPeyDal.Get(pey => pey.PeyID==id));
        }
        [HttpGet("getsonpey/{murunid}")]
        public IActionResult GetSonpey(int murunid)
        {
            KullaniciPeyDto dto = kullaniciPeyDal.GetSonpey(murunid);
            if (dto != null) return Ok(dto);
            return BadRequest("Bulunamadı");
        }
        [HttpGet("getallbykullanici/{kullaniciId}")]
        public IActionResult GetAllbyKullanici(int kullaniciId)
        {
            return Ok(kullaniciPeyDal.GetAllbyKullanici(kullaniciId));
        }
        [HttpGet("getsiparisListbykullanici/{kullaniciId}")]
        public IActionResult GetSiparisListbyKullanici(int kullaniciId)
        {
            return Ok(kullaniciPeyDal.GetSiparisListbyKullanici(kullaniciId));
        }
    }
}
