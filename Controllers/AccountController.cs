using System.Collections.Generic;
using HealthCatalystBackend.Models;
using HealthCatalystBackend.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HealthCatalystBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly AccountRepository AccountRepository;

        public AccountController(IConfiguration configuration)
        {
            AccountRepository = new AccountRepository(configuration);
        }
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return AccountRepository.GetAll();
        }

        [HttpPost]
        public ActionResult CreateAccount( Account request)
        {
            AccountRepository.CreateAccount(request);
            return Ok(request);
        }
    }
}
