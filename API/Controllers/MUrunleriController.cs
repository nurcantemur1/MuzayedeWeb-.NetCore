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
    public class MUrunleriController : ControllerBase
    {
        private EFMUrunleriDal mUrunleriDal = new EFMUrunleriDal();

        [HttpPost("add")]
        public IActionResult Add(MuzayedeUrunleri murun)
        {
            murun = mUrunleriDal.Add(murun);
            return Ok(murun);
        }
        [HttpPost("update")]
        public IActionResult Update(MuzayedeUrunleri murun)
        {
            return Ok(mUrunleriDal.Update(murun));
        }
        [HttpDelete(template: "delete/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(mUrunleriDal.Delete(mUrunleriDal.Get(urunleri => urunleri.ID == id)));
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(mUrunleriDal.GetAll());
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(mUrunleriDal.Get(urunleri => urunleri.ID == id));
        }
        [HttpGet("GetMurunId/{id}/{mid}")]
        public IActionResult GetMurunId(int id,int mid)
        {
            return Ok(mUrunleriDal.GetMurunId(id,mid));
        }
        [HttpGet("getmuzayededetay/{id}")]
        public IActionResult GetMuzayedeDetay(int id)
        {
            return Ok(mUrunleriDal.GetMuzayedeDetay(id));
        }
    }
}
