using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotNetAPI.Data
{
    class DataContextDapper
    {
        private readonly IConfiguration _config;

        public DataContextDapper(IConfiguration config)
        {
            _config = config;

        }
        public IEnumerable<T> loadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql);
        }
        public T loadSingleData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExcuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql) > 0;
        }
        public int ExcuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql);
        }
    }
}