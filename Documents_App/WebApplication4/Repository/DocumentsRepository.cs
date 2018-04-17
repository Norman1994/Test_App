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
    public class DocumentsRepository : IRepository<Documents>
    {
        private string connectionString;

        public DocumentsRepository(IConfiguration configuration, string login, string password)
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

        public void Add(Documents item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("SELECT addrecuserdocuments(@DocumentName,@Contents,@DocumentIntroNumber)", item);
            }
        }

        public IEnumerable<Documents> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Documents>("SELECT * FROM selectdata()");
            }
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

       public Documents Download(Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Documents>("SELECT * FROM selectdocument(@DocumentId)", new { DocumentId = id }).FirstOrDefault();
            }
        }

        public void Update(Documents item, Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("SELECT addlastrec(@DocumentId, @DocumentExternNumber)", item);
            }
        }

        public Documents SelectDocumentById (Guid id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Documents>("select * from selectdocumentbyid(@DocumentId)", new { DocumentId = id}).FirstOrDefault();
            }
        }

    }
}
