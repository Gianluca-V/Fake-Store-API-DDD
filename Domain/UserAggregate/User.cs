using Domain.Common.Models;
using Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Domain.UserAggregate
{
    public class User : AggregateRoot<UserId>
    {
        public string Username { get; set; } = null!;
        public UserEmail Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        private User() { }

        public static User Create(string Username, string Email, string Password)
        {
            return new User {
                Id = UserId.CreateUnique(),
                Username = Username,
                Email = UserEmail.CreateEmail(Email),
                Password = Password
            };
        }
    }
}
