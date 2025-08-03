using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace HelloWorld.Data
{
    public class DataContext
    {
        private readonly string _connectionString = @"Server=DESKTOP-DENU38E\LOCALHOST;Database=TutorialApp;TrustServerCertificate=True;Integrated Security=True;";

        public IEnumerable<T> LoadData<T>(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql, parameters);
        }

        public T? LoadDataSingle<T>(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingleOrDefault<T>(sql, parameters);
        }

        public bool ExecuteSQL(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql, parameters) > 0;
        }

        public int SQLWithRowCount(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql, parameters);
        }
    }
}