using System.Collections.Generic;
using Moq;
using TestProject.WebAPI.Controllers;
using TestProject.WebAPI.Dtos;
using TestProject.WebAPI.Logger;
using TestProject.WebAPI.Repository.Models;
using TestProject.WebAPI.Services;
using Xunit;

namespace TestProject.Tests.Unit
{

    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            var mockSimpleLogger = new Mock<ISimpleLogger>();
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object, mockSimpleLogger.Object);
        }

        [Fact(DisplayName = "Get valid user")]
        public async void GetValidUser()
        {
            // Arrange
            var email = "ken@gmail.com";
            _mockUserService.Setup(x => x.GetUser(email)).ReturnsAsync(new User
            {
                Name = "test",
                Email = email,
                MonthlySalary = 10000,
                MonthlyExpense = 1000
            });

            // Action
            var actionResult = await _userController.GetUser(email);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(actionResult?.Result);
        }


        [Fact(DisplayName = "Get an invalid user")]
        public async void GetAnInvalidUser()
        {
            // Arrange
            var email = "ken@gmail.com";
            _mockUserService.Setup(x => x.GetUser(email)).ReturnsAsync((User)null);

            // Action
            var actionResult = await _userController.GetUser(email);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundObjectResult>(actionResult?.Result);
        }


        [Fact(DisplayName = "Valid List Users")]
        public async void ValidListUsers()
        {
            // Arrange
            var email = "ken@gmail.com";
            _mockUserService.Setup(x => x.GetAllUsers()).ReturnsAsync(
                new List<User>
                {
                    new User
                    {
                        Name = "test",
                        Email = email,
                        MonthlySalary = 10000,
                        MonthlyExpense = 1000
                    }
                });

            // Action
            var actionResult = await _userController.GetAllUsers();

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(actionResult?.Result);
        }

        [Fact(DisplayName = "Valid create user")]
        public async void ValidCreateUser()
        {
            // Arrange
            var email = "ken@gmail.com";
            var request = new CreateUserRequest
            {
                Email = email,
                Name = "test",
                MonthlySalary = 10000,
                MonthlyExpense = 1000
            };
            _mockUserService.Setup(x => x.CreateUser(request)).ReturnsAsync(new CreateUserResponse { Success = true });

            // Action
            var actionResult = await _userController.CreateUser(request);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(actionResult?.Result);
        }

        [Fact(DisplayName = "InValid create user")]
        public async void InValidCreateUser()
        {
            // Arrange
            var email = "ken@gmail.com";
            var request = new CreateUserRequest
            {
                Email = email,
                Name = "test",
                MonthlySalary = 10000,
                MonthlyExpense = 1000
            };
            var errorMessage = "Error";
            _mockUserService.Setup(x => x.CreateUser(request)).ReturnsAsync(new CreateUserResponse { Success = false, ErrorMessage = errorMessage });

            // Action
            var actionResult = await _userController.CreateUser(request);

            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actionResult?.Result);
        }

    }

}