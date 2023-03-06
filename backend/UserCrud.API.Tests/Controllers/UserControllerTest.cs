using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using UserCrud.API.Controllers;
using UserCrud.Domain.Common;
using UserCrud.Domain.DTOs;
using UserCrud.Domain.Exceptions;
using UserCrud.Domain.Services;

namespace UserCrud.API.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private Mock<IUserService> mockUserService;
        private UserController controller;

        [SetUp]
        public void SetUp()
        {
            mockUserService = new Mock<IUserService>();
            controller = new UserController(mockUserService.Object);
        }

        [Test]
        public void GetUsersShouldReturnOk()
        {
            var users = new List<UserDto>()
            {
                new UserDto
                {
                    Id = 1,
                    Email = "Test@email.com",
                    Name = "Test",
                    Lastname = "Test LastName",
                    Username = "Test",
                    Role = "Admin"
                },
                new UserDto
                {
                    Id = 2,
                    Email = "Test2@email.com",
                    Name = "Test2",
                    Lastname = "Test2 LastName",
                    Username = "Test2",
                    Role = "Admin2"
                }
            };
            mockUserService.Setup(x => x.GetUsers(It.IsAny<Pagination>()))
                .ReturnsAsync(users);

            var response = controller.Get(new Pagination());
            var responseCasted = (((ObjectResult)response.Result.Result)).Value as List<UserDto>;
            Assert.IsNotNull(responseCasted);
            Assert.AreEqual(1, responseCasted.First().Id);
            Assert.AreEqual(2, responseCasted.Last().Id);
            mockUserService.Verify(x => x.GetUsers(It.IsAny<Pagination>()), Times.Once);
        }

        [Test]
        public void GetUserShouldReturnOk()
        {
            var user = new UserDto()
            {

                Id = 1,
                Email = "Test@email.com",
                Name = "Test",
                Lastname = "Test LastName",
                Username = "Test",
                Role = "Admin"
            };
            mockUserService.Setup(x => x.GetUser(It.IsAny<int>()))
                .ReturnsAsync(user);

            var response = controller.GetUserById(1);
            var responseCasted = (((ObjectResult)response.Result.Result)).Value as UserDto;
            Assert.IsNotNull(responseCasted);
            Assert.AreEqual(1, responseCasted.Id);
            Assert.AreEqual("Test", responseCasted.Name);
            mockUserService.Verify(x => x.GetUser(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetUserShouldReturnNotFound()
        {
            var user = new UserDto()
            {

                Id = 1,
                Email = "Test@email.com",
                Name = "Test",
                Lastname = "Test LastName",
                Username = "Test",
                Role = "Admin"
            };
            mockUserService.Setup(x => x.GetUser(It.IsAny<int>()))
                .ThrowsAsync(new NotFoundException("The user does not exist."));

            var response = controller.GetUserById(2);
            Assert.IsNotNull(response);
            Assert.AreEqual("The user does not exist.", response.Exception.InnerException.Message);
            Assert.AreEqual(TaskStatus.Faulted, response.Status);
            mockUserService.Verify(x => x.GetUser(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void AddUserShouldReturnOk()
        {
            var user = new UserDto()
            {
                Id = 1,
                Email = "Test@email.com",
                Name = "Test",
                Lastname = "Test LastName",
                Username = "Test",
                Role = "Admin"
            };
            mockUserService.Setup(x => x.AddUser(It.IsAny<UserDto>()))
                .ReturnsAsync(user);

            var response = controller.AddUser(new UserDto 
            {
                Id = 1,
                Email = "Test@email.com",
                Name = "Test",
                Lastname = "Test LastName",
                Username = "Test",
                Role = "Admin"
            });
            var responseCasted = (((ObjectResult)response.Result.Result)).Value as UserDto;
            Assert.IsNotNull(responseCasted);
            Assert.AreEqual(1, responseCasted.Id);
            Assert.AreEqual("Test", responseCasted.Name);
            mockUserService.Verify(x => x.AddUser(It.IsAny<UserDto>()), Times.Once);
        }

        [Test]
        public void UpdateUserShouldReturnOk()
        {
            var user = new UserDto()
            {
                Id = 1,
                Email = "Test@email.com",
                Name = "Test",
                Lastname = "Test LastName",
                Username = "Test",
                Role = "Admin"
            };
            mockUserService.Setup(x => x.UpdateUser(It.IsAny<UserDto>()))
                .ReturnsAsync(user);

            var response = controller.UpdateUser(user);
            
            Assert.IsNotNull(response);
            Assert.AreEqual(TaskStatus.RanToCompletion, response.Status);
            Assert.AreEqual(null, response.Exception);
            mockUserService.Verify(x => x.UpdateUser(It.IsAny<UserDto>()), Times.Once);
        }

        [Test]
        public void DeleteUserShouldReturnOk()
        {
            var user = new UserDto()
            {
                Id = 1,
                Email = "Test@email.com",
                Name = "Test",
                Lastname = "Test LastName",
                Username = "Test",
                Role = "Admin"
            };
            mockUserService.Setup(x => x.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(user);

            var response = controller.Delete(1);

            Assert.IsNotNull(response);
            Assert.AreEqual(TaskStatus.RanToCompletion, response.Status);
            Assert.AreEqual(null, response.Exception);
            mockUserService.Verify(x => x.DeleteUser(It.IsAny<int>()), Times.Once);
        }
    }
}
