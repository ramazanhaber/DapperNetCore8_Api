﻿using Dapper;
using Newtonsoft.Json;
using System.Data;
namespace DapperNetCore8_Api.Helper
{
    public class AsyncDatabaseHelper
    {
     public IDbConnection _connection;
        public AsyncDatabaseHelper(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<DataTable> ExecuteQueryToDataTableAsync(string query)
        {
            using var reader = await _connection.ExecuteReaderAsync(query);
            var resultTable = new DataTable();
            resultTable.Load(reader);
            return resultTable;
        }
        public async Task<string> ExecuteQueryToJsonAsync(string query)
        {
            var result = (await _connection.QueryAsync<dynamic>(query)).ToList();
            string jsonResult = JsonConvert.SerializeObject(result);
            return jsonResult;
        }
        public async Task<bool> ExecAsync(string query)
        {
            try
            {
                await _connection.ExecuteAsync(query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private string ConvertDataTableToJson(DataTable table)
        {
            return JsonConvert.SerializeObject(table);
        }
    }
}
