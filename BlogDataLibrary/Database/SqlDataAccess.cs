using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDataLibrary.Database
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public List<T> LoadData<T, U>(string sqlStatement,
                                        U parameters,
                                        string connectStringName,
                                        bool isStoredProcedure)
        {
            CommandType commandType = CommandType.Text;
            string connectionString = _config.GetConnectionString(connectStringName);

            if (isStoredProcedure)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BlogDB; Integrated Security=True;Connect Timeout=60;"))
            {
                List<T> rows = connection.Query<T>(sqlStatement, parameters,
                    commandType: commandType).ToList();
                return rows;
            }

        }

        public void SaveData<T>(string sqlStatement,
                                    T parameters,
                                    string connectStringName,
                                    bool isStoredProcedure)
        {
            string connectionString = _config.GetConnectionString(connectStringName);
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BlogDB; Integrated Security=True;Connect Timeout=60;"))
            {
                connection.Execute(sqlStatement, parameters, commandType: commandType);
            }

        }
    }
}
