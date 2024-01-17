using System.Data;
namespace DapperNetCore8_Api.Helper
{
    public class DatabaseConnections
    {
        public IDbConnection DefaultConnection { get; }
        public IDbConnection SecondConnection { get; }
        public DatabaseConnections(IDbConnection defaultConnection, IDbConnection secondConnection)
        {
            DefaultConnection = defaultConnection;
            SecondConnection = secondConnection;
        }
    }
}
