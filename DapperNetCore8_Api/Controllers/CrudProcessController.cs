﻿using Dapper;
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
    public class CrudProcessController : ControllerBase
    {
        private readonly IDbConnection _connection;
        private readonly AsyncDatabaseHelper _databaseHelper;
        private readonly IConfiguration _configuration;
        public CrudProcessController(DatabaseConnections connections, IConfiguration configuration)
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
        //[Authorize]
        [Route("GetOgrencilerGenelModel")]
        public async Task<ActionResult<GenelModel>> GetOgrencilerGenelModel()
        {
            GenelModel genelModel = new GenelModel();

            try
            {
                var ogrenciler = await _connection.QueryAsync<Ogrenciler>("SELECT * FROM Ogrenciler");
                genelModel.Data = ogrenciler;

            }
            catch (Exception ex)
            {
                genelModel.mesaj = "Başarısız";
                genelModel.durum = false;
                genelModel.hatamesaj = ex.Message;

            }
            return Ok(genelModel);

        }

        [HttpPost]
        [Route("GetOgrencilerGenelModelHatali")]
        public async Task<ActionResult<GenelModel>> GetOgrencilerGenelModelHatali()
        {
            GenelModel genelModel = new GenelModel();

            try
            {
                var ogrenciler = await _connection.QueryAsync<Ogrenciler>("SELECT * FROM Ogrenciler");
                genelModel.Data = ogrenciler;

                string sayi = "0";
                int sayi2 = 3 / Convert.ToInt32(sayi);

            }
            catch (Exception ex)
            {
                genelModel.mesaj = "Başarısız";
                genelModel.durum = false;
                genelModel.hatamesaj = ex.Message;

            }
            return Ok(genelModel);

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
            string query = "INSERT INTO Ogrenciler (Ad, Yas) OUTPUT INSERTED.id VALUES (@Ad, @Yas)";
            await _connection.ExecuteAsync(query, ogrenci);
            return Ok();
        }
        [HttpPost]
        [Route("PostOgrenciDon")]
        public async Task<ActionResult<Ogrenciler>> PostOgrenciDon(Ogrenciler ogrenci)
        {
            string query = "INSERT INTO Ogrenciler (Ad, Yas) OUTPUT INSERTED.id VALUES (@Ad, @Yas)";
            int newId = await _connection.ExecuteScalarAsync<int>(query, ogrenci);
            ogrenci.id = newId;
            return CreatedAtAction(nameof(GetOgrenciById), new { id = ogrenci.id }, ogrenci);
        }
        [HttpPost]
        [Route("UpdateOgrenci")]
        public async Task<IActionResult> UpdateOgrenci(Ogrenciler ogrenci)
        {
            string query = "UPDATE Ogrenciler SET Ad = @Ad, Yas = @Yas WHERE id = @id";
            await _connection.ExecuteAsync(query, ogrenci);
            return Ok();
        }
        [HttpPost]
        [Route("DeleteOgrenci")]
        public async Task<IActionResult> DeleteOgrenci(int id)
        {
            string query = "DELETE FROM Ogrenciler WHERE id = @id";
            await _connection.ExecuteAsync(query, new { id = id });
            return Ok();
        }
        [HttpPost]
        [Route("QueryToJsonveQueryToDataTableveExec")]
        public async Task<IActionResult> QueryToJsonveQueryToDataTableveExec(string query)
        {
            string json = await _databaseHelper.ExecuteQueryToJsonAsync(query);
            DataTable dataTable = await _databaseHelper.ExecuteQueryToDataTableAsync(query);
            bool sonuc = await _databaseHelper.ExecAsync(query);
            return Ok(json);
        }

        [HttpPost]
        [Route("imlementQuery")]
        public async Task<IActionResult> imlementQuery()
        {
            var ogrenci = await _databaseHelper._connection.QuerySingleOrDefaultAsync<Ogrenciler>("SELECT * FROM Ogrenciler ");
            if (ogrenci == null)
            {
                return NotFound();
            }
            return Ok(ogrenci);

        }

        [HttpPost]
        [Route("dinamikconnection")]
        public async Task<IActionResult> dinamikconnection(string query)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            var ogrenciler = await dbConnection.QueryAsync<Ogrenciler>("SELECT * FROM Ogrenciler");
            return Ok(ogrenciler);
        }


        [HttpPost]
        [Route("Data2")]
        public async Task<ActionResult<GenelModel>> Data2()
        {
            var ogrenciler = await _connection.QueryAsync<Ogrenciler>("SELECT * FROM Ogrenciler");
            GenelModel genelModel = new GenelModel();
            genelModel.Data = ogrenciler.FirstOrDefault();
            var jsonText = JsonConvert.SerializeObject(genelModel).Replace(",\"Data\":", ",\"Ogrenciler\":");
            return Ok(jsonText);
        }
        [HttpPost]
        [Route("Data3")]
        public async Task<ActionResult<dynamic>> Data3()
        {
            var ogrenciler = await _connection.QueryAsync<Ogrenciler>("SELECT * FROM Ogrenciler");
            var genelmodel = new
            {
                durum = true,
                mesaj = "Başarılı",
                hatamesaj = "",
                ogrenciler1 = ogrenciler,
            };
            return genelmodel;
        }
    }
}
