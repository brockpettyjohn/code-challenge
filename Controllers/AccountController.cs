using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HealthCatalystBackend.Models;
using HealthCatalystBackend.Repository;
using Microsoft.AspNetCore.Http;
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

        // [HttpPost]
        // [Route("image-upload")]
        // public ActionResult UploadImage()
        // {
        //     AccountRepository.UploadImage(Request.Body);
        //     return Ok();
        // }
    }
}
