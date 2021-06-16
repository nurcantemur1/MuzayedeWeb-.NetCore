using Microsoft.AspNetCore.Mvc;
using Data.Access;
using Data.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KartBilgileriController : Controller
    {
        private EFKartBilgileriDal kartBilgileriDal = new EFKartBilgileriDal();

        [HttpPost("add")]
        public IActionResult Add(KartBilgileri kartBilgileri)
        {
            kartBilgileri = kartBilgileriDal.Add(kartBilgileri);
            return Ok(kartBilgileri);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(kartBilgileriDal.GetAll());
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(kartBilgileriDal.Get(kartBilgileri => kartBilgileri.Id == id));
        }
        [HttpPost("update")]
        public IActionResult Update(KartBilgileri dto)
        {
            return Ok(kartBilgileriDal.Update(dto));
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(kartBilgileriDal.Delete(kartBilgileriDal.Get(kartBilgileri => kartBilgileri.Id == id)));
        }
        
        [HttpGet("getVarsayilanKart/{KullaniciId}")]
        public IActionResult GetVarsayilanKart(int KullaniciId)
        {
            return Ok(kartBilgileriDal.GetDefaultByKullaniciId(KullaniciId));
            
        }

        [HttpGet("getAllbykullanici/{KullaniciId}")]
        public IActionResult GetAllbyKullanici(int KullaniciId)
        {
            return Ok(kartBilgileriDal.GetAllByKullaniciId(KullaniciId));

        }

        [HttpPost("varsayilanyap/{id}")]
        public IActionResult VarsayilanYap(int id)
        {
            return Ok(kartBilgileriDal.VarsayilanYap(id));
        }
    }
}
