using System.Threading.Tasks;
using Mofleet.Models.TokenAuth;
using Mofleet.Web.Controllers;
using Shouldly;
using Xunit;

namespace Mofleet.Web.Tests.Controllers
{
    public class HomeController_Tests: MofleetWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}