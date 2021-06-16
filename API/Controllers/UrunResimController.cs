using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Data.Access;
using Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunResimController : ControllerBase
    {
        private EFUrunResimDal urunResimDal = new EFUrunResimDal();

       

        [HttpPost("addresim")]
        public IActionResult AddResim(Resim resim)
        {
            return Ok(urunResimDal.AddResim(resim.ResimID, resim.Base64));
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(urunResimDal.GetAll());
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(urunResimDal.Get(urunResim => urunResim.UrunResimID == id));
        }
        [HttpPost("update")]
        public IActionResult Update(UrunResim dto)
        {
            urunResimDal.Update(dto);
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            urunResimDal.Delete(urunResimDal.Get(urunResim => urunResim.UrunResimID == id));
            return Ok();
        }
        [HttpGet("geturunresim/{id}")]
        public IActionResult GetUrunResim(int id)
        {
            return Ok(urunResimDal.GetUrunResim(id));
        }
        
    }
}
