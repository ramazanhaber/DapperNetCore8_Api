using Dapper;
using Dapper.Contrib.Extensions;
using DapperNetCore8_Api.Helper;
using DapperNetCore8_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
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
            _connection = connections.DefaultConnection; // ilk veri tabanı
            _databaseHelper = new AsyncDatabaseHelper(connections.SecondConnection); // ikinci veri tabanı
            _configuration = configuration;


        }

        [HttpPost]
        [Route("GetOgrenciler")]
        public async Task<ActionResult<IEnumerable<Ogrenciler>>> GetOgrenciler()
        {
            var ogrenciler = await _connection.QueryAsync<Ogrenciler>("SELECT * FROM Ogrenciler");
            return Ok(ogrenciler);
        }


        [HttpPost]
        [Route("GetOgrenciById")]
        public async Task<ActionResult<Ogrenciler>> GetOgrenciById(int id)
        {
            var ogrenci = await _connection.QuerySingleOrDefaultAsync<Ogrenciler>("SELECT * FROM Ogrenciler WHERE id = @id", new { id = id });
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

            await _connection.DeleteAsync<Ogrenciler>(new Ogrenciler { id = id });

            return Ok();
        }



        [HttpPost]
        [Route("DeleteOgrenciDynamicParameters")]
        public async Task<IActionResult> DeleteOgrenciDynamicParameters(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32);

            await _connection.ExecuteAsync("DELETE FROM Ogrenciler WHERE id = @id", parameters);

            return Ok();
        }


    }
}
