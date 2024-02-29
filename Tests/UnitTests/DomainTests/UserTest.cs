using Domain.UserAggregate.ValueObjects;
using Domain.UserAggregate;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Domain.Common.Exceptions;

namespace Tests.UnitTests.DomainTest
{
    public class UserTest
    {
        [Fact]
        public void Create_User_Should_Return_User()
        {

            User user = User.Create(DomainUtils.UserName, DomainUtils.UserEmail,DomainUtils.UserPassword);

            Assert.IsType<UserId>(user.Id);
            Assert.Equal(DomainUtils.UserName, user.username);
            Assert.Equal(DomainUtils.UserEmail, user.email.value);
            Assert.Equal(DomainUtils.UserPassword, user.password);
            Assert.IsType<User>(user);
        }

        [Fact]
        public void Create_User_Id_Unique_Should_Return_User_Id()
        {
            UserId userId = UserId.CreateUnique();

            Assert.IsType<UserId>(userId);
        }

        [Fact]
        public void Create_User_Email_Should_Return_User_Email()
        {
            UserEmail userEmail = UserEmail.CreateEmail(DomainUtils.UserEmail);

            Assert.Equal(DomainUtils.UserEmail, userEmail.value);
            Assert.IsType<UserEmail>(userEmail);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("userEmail")]
        [InlineData("userEmail.com")]
        [InlineData("userEmail@.com")]
        [InlineData("@userEmail.com")]
        [InlineData("userEmail.com@")]
        [InlineData("us@erEma@il.com")]
        [InlineData("user@Email.c")]
        [InlineData("user@Email .com")]
        public void Create_User_Email_Should_Throw_Exception_On_Invalid_Email(string email)
        {
            Assert.Throws<ValidationException>(() => { UserEmail.CreateEmail(email); });
        }
    }
}
