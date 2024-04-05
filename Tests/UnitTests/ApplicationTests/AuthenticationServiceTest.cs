using Application.Common.Interfaces.Persistence;
using Application.Services.AuthenticationService;
using Domain.Common.Exceptions;
using Domain.UserAggregate;
using Infrastructure.Authentication;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.UnitTests.InfrastructureTest;
using Xunit;

namespace Tests.UnitTests.ApplicationTests
{
    public class AuthenticationServiceTest
    {
        [Fact]
        public async void Register_WhenEmailDoesNotExist_ShouldReturnAuthenticationResult()
        {
            //Arrange
            JWTTokenGenerator tokenGenerator = new JWTTokenGenerator(Options.Create(InfrastructureUtils.JwtSettings));
            var userRepository = new Mock<IUserRepository>();
            var authenticationService = new AuthenticationService(tokenGenerator,userRepository.Object);

            User expectedUser = null;
            userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(expectedUser);

            //Act
            var actualUser = await authenticationService.Register(ApplicationUtils.UserName,ApplicationUtils.UserEmail,ApplicationUtils.UserPassword);

            //Assert
            Assert.IsType<AuthenticationResult>(actualUser);
            Assert.NotNull(actualUser);
            Assert.NotNull(actualUser.user);
            Assert.NotEmpty(actualUser.Token);
        }

        [Fact]
        public async void Register_WhenEmailDoesExist_ShouldThrowAlreadyExistException()
        {
            //Arrange
            JWTTokenGenerator tokenGenerator = new JWTTokenGenerator(Options.Create(InfrastructureUtils.JwtSettings));
            var userRepository = new Mock<IUserRepository>();
            var authenticationService = new AuthenticationService(tokenGenerator, userRepository.Object);

            User expectedUser = ApplicationUtils.User;
            userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(expectedUser);

            //Act
            var exception = await Assert.ThrowsAsync<AlreadyExistException>(async () => await authenticationService.Register(ApplicationUtils.UserName, ApplicationUtils.UserEmail, ApplicationUtils.UserPassword));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("User with given email already exist", exception.Message);
        }

        [Fact]
        public async void Login_WhenEmailAndPasswordAreCorrect_ShouldReturnAuthenticationResult()
        {
            //Arrange
            JWTTokenGenerator tokenGenerator = new JWTTokenGenerator(Options.Create(InfrastructureUtils.JwtSettings));
            var userRepository = new Mock<IUserRepository>();
            var authenticationService = new AuthenticationService(tokenGenerator, userRepository.Object);

            User expectedUser = ApplicationUtils.User;
            userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(expectedUser);

            //Act
            var actualUser = await authenticationService.Login(ApplicationUtils.UserEmail, ApplicationUtils.UserPassword);

            //Assert
            Assert.IsType<AuthenticationResult>(actualUser);
            Assert.NotNull(actualUser);
            Assert.NotNull(actualUser.user);
            Assert.NotEmpty(actualUser.Token);
        }

        [Fact]
        public async void Login_WhenEmailIsCorrectAndPasswordIsIncorrect_ShouldThrowLoginException()
        {
            //Arrange
            JWTTokenGenerator tokenGenerator = new JWTTokenGenerator(Options.Create(InfrastructureUtils.JwtSettings));
            var userRepository = new Mock<IUserRepository>();
            var authenticationService = new AuthenticationService(tokenGenerator, userRepository.Object);

            User expectedUser = ApplicationUtils.User;
            userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(expectedUser);

            //Act
            var exception = await Assert.ThrowsAsync<LoginException>(async () => await authenticationService.Login(ApplicationUtils.UserEmail, "1234"));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Invalid user credentials", exception.Message);
        }

        [Fact]
        public async void Login_WhenEmailIsIncorrectAndPasswordIsCorrect_ShouldThrowLoginException()
        {
            //Arrange
            JWTTokenGenerator tokenGenerator = new JWTTokenGenerator(Options.Create(InfrastructureUtils.JwtSettings));
            var userRepository = new Mock<IUserRepository>();
            var authenticationService = new AuthenticationService(tokenGenerator, userRepository.Object);

            User expectedUser = null;
            userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(expectedUser);

            //Act
            var exception = await Assert.ThrowsAsync<LoginException>(async () => await authenticationService.Login("incorrect@email.com", ApplicationUtils.UserPassword));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Invalid user credentials", exception.Message);
        }

        [Fact]
        public async void Login_WhenEmailIsIncorrectAndPasswordIsIncorrect_ShouldThrowLoginException()
        {
            //Arrange
            JWTTokenGenerator tokenGenerator = new JWTTokenGenerator(Options.Create(InfrastructureUtils.JwtSettings));
            var userRepository = new Mock<IUserRepository>();
            var authenticationService = new AuthenticationService(tokenGenerator, userRepository.Object);

            User expectedUser = null;
            userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(expectedUser);

            //Act
            var exception = await Assert.ThrowsAsync<LoginException>(async () => await authenticationService.Login("incorrect@email.com", "1234"));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Invalid user credentials", exception.Message);
        }
    }
}
