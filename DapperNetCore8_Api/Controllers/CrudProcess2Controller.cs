using Dapper;
using Dapper.Contrib.Extensions;
using DapperNetCore8_Api.Helper;
using DapperNetCore8_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
namespace DapperNetCore8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudProcess2Controller : ControllerBase
    {
        private readonly IDbConnection _connection;
        private readonly AsyncDatabaseHelper _databaseHelper;
        private readonly IConfiguration _configuration;
        public CrudProcess2Controller(DatabaseConnections connections, IConfiguration configuration)
        {
            _connection = connections.DefaultConnection; 
            _databaseHelper = new AsyncDatabaseHelper(connections.SecondConnection); // ikinci veri tabanı
            _configuration = configuration;
        }
        [HttpGet] 
        [Route("GetOgrenciler")]
        public async Task<ActionResult<IEnumerable<Ogrenciler>>> GetOgrenciler()
        {
            var ogrenciler = await _connection.GetAllAsync<Ogrenciler>();
            return Ok(ogrenciler);
        }
        [HttpPost]
        [Route("GetOgrenciById")]
        public async Task<ActionResult<Ogrenciler>> GetOgrenciById(int id)
        {
            var ogrenci = await _connection.GetAsync<Ogrenciler>(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            return Ok(ogrenci);
        }
        [HttpPost]
        [Route("PostOgrenci")]
        public async Task<IActionResult> PostOgrenci(Ogrenciler ogrenci)
        {
            await _connection.InsertAsync(ogrenci);
            return Ok();
        }
        [HttpPost]
        [Route("UpdateOgrenci")]
        public async Task<IActionResult> UpdateOgrenci(Ogrenciler ogrenci)
        {
            await _connection.UpdateAsync(ogrenci);
            return Ok();
        }
        [HttpPost]
        [Route("DeleteOgrenci")]
        public async Task<IActionResult> DeleteOgrenci(int id)
        {
            var result = await _connection.DeleteAsync<Ogrenciler>(new Ogrenciler { id = id });
            return result ? Ok() : NotFound();
        }
        [HttpPost]
        [Route("DeleteOgrenciProc")]
        public async Task<IActionResult> DeleteOgrenciProc(int id)
        {
            var affectedRows = await _connection.ExecuteAsync("deleteogrenci", new { id = id }, commandType: CommandType.StoredProcedure);
            return affectedRows > 0 ? Ok() : NotFound();
        }
        [HttpPost]
        [Route("DeleteOgrenciDynamicParameters")]
        public async Task<IActionResult> DeleteOgrenciDynamicParameters(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32);
            var affectedRows = await _connection.ExecuteAsync("DELETE FROM Ogrenciler WHERE id = @id", parameters);
            return affectedRows > 0 ? Ok() : NotFound();
        }
    }
}
