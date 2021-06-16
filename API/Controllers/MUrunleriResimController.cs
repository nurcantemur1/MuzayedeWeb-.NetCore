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
    public class MUrunleriResimController : ControllerBase
    {
        private EFMUrunleriResimDal dal = new EFMUrunleriResimDal();


        [HttpPost("addresim")]
        public IActionResult AddResim(Resim resim)
        {
            return Ok(dal.AddResim(resim.ResimID, resim.Base64));
        }
        [HttpPost("add")]
        public IActionResult Add(MUrunleriResim resim)
        {
            return Ok(dal.Add(resim));
        }
        [HttpGet("getmurunresim/{id}")]
        public IActionResult GetMUrunResim(int id)
        {
            return Ok(dal.GetMUrunResim(id));
        }
      
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(dal.GetAll());
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(dal.Get(kategori => kategori.MurunResimID == id));
        }
    }
}
