using MCE_ASP_NET_MVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewResultNotNull()
        {
            // Arrange
            HomeController homeController = new HomeController();

            // Act
            ViewResult result = homeController.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
