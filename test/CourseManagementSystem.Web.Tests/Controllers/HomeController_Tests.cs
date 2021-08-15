using System.Threading.Tasks;
using CourseManagementSystem.Models.TokenAuth;
using CourseManagementSystem.Web.Controllers;
using Shouldly;
using Xunit;

namespace CourseManagementSystem.Web.Tests.Controllers
{
    public class HomeController_Tests: CourseManagementSystemWebTestBase
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