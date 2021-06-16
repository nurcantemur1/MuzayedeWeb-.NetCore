using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Access;
using Data.Entities;
using Data.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunController : ControllerBase
    {
        private EFUrunDal urunDal = new EFUrunDal();
        
        [HttpPost("add")]
        public IActionResult Add(Urun urun)
        {
            urun = urunDal.Add(urun);
            return Ok(urun);
        }

        [HttpPost("update")]
        public IActionResult Update(Urun urun)
        {
           
            return Ok( urunDal.Update(urun));
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(urunDal.GetAll());
        }
        
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(urunDal.Get(urun => urun.UrunID == id));
        }
        
        [HttpGet("GetList/{kid}")]
        public IActionResult GetList(int kid)
        {
            return Ok(urunDal.GetAllbyKullanici(kid));
        }
        [HttpGet("getallbykullanicimuzayedesiz/{kid}")]
        public IActionResult GetAllbyKullaniciMuzayedesiz(int kid)
        {
            return Ok(urunDal.GetAllbyKullaniciMuzayedesiz(kid));
        }
        [HttpDelete(template: "delete/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(urunDal.Delete(urunDal.Get(urun => urun.UrunID == id)));
        }
      
    }
}
