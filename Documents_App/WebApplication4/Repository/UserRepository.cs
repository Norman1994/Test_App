using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Entites;

namespace WebApplication4.Repository
{
    public class UserRepository : IRepository<Users>
    {
        private string connectionString;

        public UserRepository(IConfiguration configuration, string login, string password)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            connectionString = connectionString.Replace("username", login);
            connectionString = connectionString.Replace("userpassword", password);
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public Users Add(Users item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("SELECT createusertables (@UserName,@TableName)", item);
            }
            return item;
        }

        public IEnumerable<Users> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Users>("SELECT * FROM user_app");
            }
        }

        public void Remove(Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("SELECT * from deleteusers(@IdUser)", new { IdUser = id });
            }
        }

        void IRepository<Users>.Add(Users item)
        {
            throw new NotImplementedException();
        }
    }
}
