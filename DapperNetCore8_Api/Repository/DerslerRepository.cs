using Dapper.Contrib.Extensions;
using DapperNetCore8_Api.Helper;
using DapperNetCore8_Api.Interfaces;
using DapperNetCore8_Api.Models;
using System.Data;

namespace DapperNetCore8_Api.Repository
{
    public class DerslerRepository : GenericRepository<Dersler>, IDerslerRepository
    {
        public DerslerRepository(DatabaseConnections connections, IConfiguration configuration) : base(connections, configuration)
        {
        }
    }
}
