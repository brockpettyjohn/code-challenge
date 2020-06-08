using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using HealthCatalystBackend.Models;
using System;
using System.IO;

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
            string imageName = null;
            imageName = new String(Path.GetFileNameWithoutExtension(item.ImageUrl).Take(10).ToArray()).Replace(" ", ".");
            item.ImageUrl = imageName;
            imageName = imageName + DateTime.Now.ToString("yymmssfff");

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO Account (firstname, lastname, address, age, interests, imageurl ) VALUES(@FirstName,@LastName,@Address,@Age,@Interests,@ImageUrl)",
                new
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    Age = item.Age,
                    Interests = item.Interests,
                    ImageUrl = item.ImageUrl
                });
            }

        }

        // public void UploadImage(Stream imageStream)
        // {
        //     StreamReader reader = new StreamReader(imageStream);
        //     string text = reader.ReadToEnd();

        //     var bytes = Convert.FromBase64String(text);
        //     using (var imageFile = new FileStream(filePath, FileAccess.Write))
        //     {
        //         imageFile.Write(bytes, 0, bytes.Length);
        //         imageFile.Flush();
        //     }
        // }







    }
}