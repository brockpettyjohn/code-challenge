using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using HealthCatalystBackend.Models;

namespace HealthCatalystBackend.Repository
{
    public class AccountRepository : IRepository<Account>
    {
        private string connectionString;
        public AccountRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }


        public IEnumerable<Account> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Account>("SELECT * FROM Account");
            }
        }

        public void CreateAccount(Account item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO Account (firstname, lastname, address, age, interests ) VALUES(@FirstName,@LastName,@Address,@Age,@Interests)",
                new
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    Age = item.Age,
                    Interests = item.Interests
                });
            }

        }







    }
}