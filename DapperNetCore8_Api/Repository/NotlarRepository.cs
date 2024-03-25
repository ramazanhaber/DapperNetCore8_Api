using Dapper.Contrib.Extensions;
using DapperNetCore8_Api.Helper;
using DapperNetCore8_Api.Interfaces;
using DapperNetCore8_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DapperNetCore8_Api.Repository
{
    public class NotlarRepository :INotlarRepository
    {
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;
        public NotlarRepository(DatabaseConnections connections, IConfiguration configuration)
        {
            _connection = connections.DefaultConnection;
            _configuration = configuration;
        }

        public async Task<int> AddAsync(Notlar entity)
        {
            int sonuc = await _connection.InsertAsync(entity);

            return sonuc;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = await _connection.DeleteAsync<Notlar>(new Notlar { id = id });
            return result;
        }

        public async Task<IEnumerable<Notlar>> GetAllAsync()
        {
            var ogrenciler = await _connection.GetAllAsync<Notlar>();
            return ogrenciler;
        }

        public async Task<Notlar> GetByIdAsync(int id)
        {
            var ogrenci = await _connection.GetAsync<Notlar>(id);
            if (ogrenci == null)
            {
                return new Notlar();
            }
            return ogrenci;
        }


        public async Task<bool> UpdateAsync(Notlar entity)
        {
            bool sonuc = await _connection.UpdateAsync(entity);
            return sonuc;
        }
    }
}
