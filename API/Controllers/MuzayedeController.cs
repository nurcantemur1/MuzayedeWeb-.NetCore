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
    public class MuzayedeController : ControllerBase
    {
        private EFMuzayedeDal muzayedeDal = new EFMuzayedeDal();


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(muzayedeDal.GetAll());
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(muzayedeDal.Get(muzayede => muzayede.MuzayedeID == id));
        }
        
        [HttpGet("kmgetall/{kid}")]
        public IActionResult KMGetAll(int kId)
        {
            return Ok(muzayedeDal.GetAllKullaniciMuzayedeleri(kId).ToList());
        }
        
        [HttpDelete(template: "delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (muzayedeDal.Delete(muzayedeDal.Get(muzayede => muzayede.MuzayedeID == id)))
            {
                EFMUrunleriDal mUrunleriDal = new EFMUrunleriDal();
                List<MuzayedeUrunleri> list = mUrunleriDal.GetAll().Where(x=>x.MuzayedeID == id).ToList();
                foreach (MuzayedeUrunleri murun in list)
                {
                    mUrunleriDal.Delete(murun);
                }
                return Ok(true);
            }

            return BadRequest(false);
        }
        [HttpPost("add")]
        public IActionResult Add(Muzayede muzayede)
        {
            muzayede = muzayedeDal.Add(muzayede);
            return Ok(muzayede);
        }
        [HttpPost("addmuzayede")]
        public IActionResult AddMuzayede(Muzayede muzayede)
        {
            muzayede = muzayedeDal.AddMuzayede(muzayede);
            return Ok(muzayede);
        }

        [HttpPost("update")]
        public IActionResult UpdateMuzayede(Muzayede muzayede)
        {
            return Ok(muzayedeDal.UpdateMuzayede(muzayede));
        }

    }
}
