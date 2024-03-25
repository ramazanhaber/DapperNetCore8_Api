using DapperNetCore8_Api.Interfaces;
using DapperNetCore8_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperNetCore8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudProcess3InterfacesController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;

        public CrudProcess3InterfacesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetOgrenciler")]
        public async Task<ActionResult<IEnumerable<Ogrenciler>>> GetOgrenciler()
        {
            var ogrenciler = await unitOfWork.Ogrenciler.GetAllAsync();
            return Ok(ogrenciler);
        }

        [HttpGet]
        [Route("GetNotlar")]
        public async Task<ActionResult<IEnumerable<Notlar>>> GetNotlar()
        {
            var notlar = await unitOfWork.Notlar.GetAllAsync();
            return Ok(notlar);
        }

        [HttpGet]
        [Route("GetDersler")]
        public async Task<ActionResult<IEnumerable<Dersler>>> GetDersler()
        {
            var notlar = await unitOfWork.Dersler.GetAllAsync();
            return Ok(notlar);
        }


        [HttpPost]
        [Route("AddOgrenci")]
        public async Task<IActionResult> AddOgrenci(Ogrenciler ogrenci)
        {
            await unitOfWork.Ogrenciler.AddAsync(ogrenci);
            return Ok();
        }
        [HttpPost]
        [Route("UpdateOgrenci")]
        public async Task<IActionResult> UpdateOgrenci(Ogrenciler ogrenci)
        {
            await unitOfWork.Ogrenciler.UpdateAsync(ogrenci);
            return Ok();
        }
        [HttpPost]
        [Route("DeleteOgrenci")]
        public async Task<IActionResult> DeleteOgrenci(int id)
        {
            var result = await unitOfWork.Ogrenciler.DeleteAsync(id);
            return result ? Ok() : NotFound();
        }

    }
}
