using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace HealthCatalystBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            using (var connection = new NpgsqlConnection("Host=finaldb.c065rhxjszi3.us-west-2.rds.amazonaws.com;Port=5432;Database=postgres;User Id=brock;Password=M!ckD0ngles;"))
            {
                return connection.Query<Account>("SELECT * FROM Account");
            }
        }

        [HttpPost]
        public void CreateAccount(Account request)
        {
            using (var connection = new NpgsqlConnection("Host=finaldb.c065rhxjszi3.us-west-2.rds.amazonaws.com;Port=5432;Database=postgres;User Id=brock;Password=M!ckD0ngles;"))
            {
                var newAccount = new
                {
                    request.FirstName,
                    request.LastName,
                    request.Age,
                    request.Interests
                };

                connection.Open();
                // connection.Query<Account>("INSERT INTO customer (name,phone,email,address) VALUES(@FirstName,@LastName,@Address,@Age,@Interest)", newAccount);
                connection.Query<Account>("INSERT INTO customer (name,phone,email,address) VALUES('Chase','Pettyjohn','where the heart is','30','knives')");
            }
        }
    }



    public class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Interests { get; set; }
    }
}
