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
    public class KategoriController : ControllerBase
    {
        private EFKategoriDal kategoriDal = new EFKategoriDal();
        
        [HttpPost("add")]
        public IActionResult Add(Kategori kategori)
        {
            kategori=  kategoriDal.Add(kategori);
            return Ok(kategori);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(kategoriDal.GetAll());
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(kategoriDal.Get(kategori => kategori.KategoriID==id));
        }
        [HttpPost("update")]
        public IActionResult Update(Kategori dto)
        {
            kategoriDal.Update(dto);
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            kategoriDal.Delete(kategoriDal.Get(kategori => kategori.KategoriID == id));
            return Ok();
        }
    }
}
