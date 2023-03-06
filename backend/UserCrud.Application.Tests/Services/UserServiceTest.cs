namespace UserCrud.Application.Tests.Services
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using UserCrud.Application.Services;
    using UserCrud.Domain.Common;
    using UserCrud.Domain.DTOs;
    using UserCrud.Domain.Entities;
    using UserCrud.Domain.Repositories;
    using UserCrud.Domain.Services;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IRepository<User>> _mockUserRepository;
        private IMapper _mapper;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            var mapperConfig = new MapperConfiguration(x => x.CreateMap<UserDto, User>().ReverseMap());
            _mapper = mapperConfig.CreateMapper();
            _userService = new UserService(_mockUserRepository.Object, _mapper);
        }

        [Test]
        public void GetUserShouldReturnOk()
        {
            _mockUserRepository.Setup(f => f.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(true);

            _mockUserRepository.Setup(f => f.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetUser());

            var response = _userService.GetUser(1);
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Result.Id);
            Assert.AreEqual("test", response.Result.Name);
            _mockUserRepository.Verify(x => x.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            _mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void AddUserShouldReturnOk()
        {
            _mockUserRepository.Setup(f => f.AddAsync(It.IsAny<User>()));

            var response = _userService.AddUser(GetUserDto());
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Result.Id);
            Assert.AreEqual("test", response.Result.Name);
            _mockUserRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void UpdateUserShouldReturnOk()
        {
            _mockUserRepository.Setup(f => f.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(true);
            _mockUserRepository.Setup(f => f.UpdateAsync(It.IsAny<User>()));

            var response = _userService.UpdateUser(GetUserDto());
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Result.Id);
            Assert.AreEqual("test", response.Result.Name);
            _mockUserRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void DeleteUserShouldReturnOk()
        {
            _mockUserRepository.Setup(f => f.ExistAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(true);
            _mockUserRepository.Setup(f => f.DeleteAsync(It.IsAny<User>()));

            var response = _userService.DeleteUser(1);
            Assert.IsNotNull(response);
            _mockUserRepository.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);
        }

        private User GetUser()
        {
            return new User
            {
                Id = 1,
                Lastname = "test",
                Role = "Admin",
                Username = "tester",
                Name = "test",
                Email = "test@hotmail.com"
            };
        }

        private UserDto GetUserDto()
        {
            return new UserDto
            {
                Id = 1,
                Lastname = "test",
                Role = "Admin",
                Username = "tester",
                Name = "test",
                Email = "test@hotmail.com"
            };
        }
    }
}
