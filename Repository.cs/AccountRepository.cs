using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using HealthCatalystBackend.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace HealthCatalystBackend.Repository
{
    public class AccountRepository : IRepository<Account>
    {

        private string connectionString;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AccountRepository(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            _hostingEnvironment = hostingEnvironment;
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

        public void AddFile(IFormFile file)
        {
            if (file == null) throw new Exception("File is null");
            if (file.Length == 0) throw new Exception("File is empty");

            using (Stream stream = file.OpenReadStream())
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var fileContent = binaryReader.ReadBytes((int)file.Length);
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath + "/Images/", file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);
                    }
                }
            }
        }
    }
}