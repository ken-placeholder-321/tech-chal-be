using Moq;
using TestProject.WebAPI.Dtos;
using TestProject.WebAPI.Repository;
using TestProject.WebAPI.Repository.Models;
using TestProject.WebAPI.Services;
using Xunit;

namespace TestProject.Tests.Unit
{

    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }


        [Fact(DisplayName = "Create user that's valid")]
        public async void CreateUserValid()
        {
            // Arrange
            var email = "ken@gmail.com";
            var request = new CreateUserRequest
            {
                Email = email,
                Name = "Name",
                MonthlySalary = 10000,
                MonthlyExpense = 1000
            };
            _mockUserRepository.Setup(x => x.CheckIfUserExists(email)).ReturnsAsync(false);

            // Action
            var actionResult = await _userService.CreateUser(request);

            // Assert
            _mockUserRepository.Verify(_ => _.CreateUser(It.IsAny<User>()), Times.Once);
            Assert.True(actionResult.Success);
        }



        [Fact(DisplayName = "Create user that's invalid - exist already")]
        public async void CreateUserInvalidExistsAlready()
        {
            // Arrange
            var email = "ken@gmail.com";
            var request = new CreateUserRequest
            {
                Email = email,
                Name = "Name",
                MonthlySalary = 10000,
                MonthlyExpense = 1000
            };
            var expectedErrorMessage = $"{request.Email} already exists in our system";
            _mockUserRepository.Setup(x => x.CheckIfUserExists(email)).ReturnsAsync(true);

            // Action
            var actionResult = await _userService.CreateUser(request);

            // Assert
            Assert.False(actionResult.Success);
            Assert.Equal(expectedErrorMessage, actionResult.ErrorMessage);
        }


        [Fact(DisplayName = "Create user that's invalid - negative salary")]
        public async void CreateUserInvalidNegativeSalary()
        {
            // Arrange
            var email = "ken@gmail.com";
            var request = new CreateUserRequest
            {
                Email = email,
                Name = "Name",
                MonthlySalary = -1000,
                MonthlyExpense = 1000
            };
            var expectedErrorMessage = $"Monthly Salary cannot be negative";
            _mockUserRepository.Setup(x => x.CheckIfUserExists(email)).ReturnsAsync(false);

            // Action
            var actionResult = await _userService.CreateUser(request);

            // Assert
            Assert.False(actionResult.Success);
            Assert.Equal(expectedErrorMessage, actionResult.ErrorMessage);
        }

        [Fact(DisplayName = "Create user that's invalid - negative expense")]
        public async void CreateUserInvalidNegativeExpense()
        {
            // Arrange
            var email = "ken@gmail.com";
            var request = new CreateUserRequest
            {
                Email = email,
                Name = "Name",
                MonthlySalary = 1000,
                MonthlyExpense = -1000
            };
            var expectedErrorMessage = $"Monthly Expense cannot be negative";
            _mockUserRepository.Setup(x => x.CheckIfUserExists(email)).ReturnsAsync(false);

            // Action
            var actionResult = await _userService.CreateUser(request);

            // Assert
            Assert.False(actionResult.Success);
            Assert.Equal(expectedErrorMessage, actionResult.ErrorMessage);
        }

        [Fact(DisplayName = "Create user that's invalid - salary vs expense does not meet business requirements")]
        public async void CreateUserInvalidSalaryExpense()
        {
            // Arrange
            var email = "ken@gmail.com";
            var request = new CreateUserRequest
            {
                Email = email,
                Name = "Name",
                MonthlySalary = 1000,
                MonthlyExpense = 500
            };
            var expectedErrorMessage = $"Monthly Salary and Expense do not satisfy our requirements";
            _mockUserRepository.Setup(x => x.CheckIfUserExists(email)).ReturnsAsync(false);
            
            // Action
            var actionResult = await _userService.CreateUser(request);

            // Assert
            Assert.False(actionResult.Success);
            Assert.Equal(expectedErrorMessage, actionResult.ErrorMessage);
        }

    }

}