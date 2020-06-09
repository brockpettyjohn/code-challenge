using HealthCatalystBackend.Repository;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using HealthCatalystBackend.Controllers;

public class RepositoryTest
{
    private IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public RepositoryTest(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
    {
        _configuration = configuration;
        _hostingEnvironment = hostingEnvironment;
    }

    [Fact]
    public void GetAccountsTest()
    {

        AccountController obj = new AccountController(_configuration, _hostingEnvironment);
        var result = obj.Get();


        Assert.Collection(result, item => Assert.Contains("John", item.FirstName),
                                      item => Assert.Contains("Smith", item.LastName),
                                      item => Assert.Contains("3000 e. main", item.Address),
                                      item => Assert.Contains("running", item.LastName),
                                      item => Assert.Contains("img.jpg", item.ImageUrl)
                                      );
    }
}