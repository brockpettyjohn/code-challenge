using System.Collections.Generic;
using HealthCatalystBackend.Models;
using HealthCatalystBackend.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HealthCatalystBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly AccountRepository AccountRepository;

        public AccountController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            AccountRepository = new AccountRepository(configuration, hostingEnvironment);
        }
        
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return AccountRepository.GetAll();
        }

        [HttpPost]
        public ActionResult CreateAccount(Account request)
        {
            AccountRepository.CreateAccount(request);
            return Ok(request);
        }

        [HttpPost]
        [Route("image-upload")]
        public ActionResult UploadImage(IFormFile file)
        {
           AccountRepository.AddFile(file);
           return Ok();
        }
    }
}
