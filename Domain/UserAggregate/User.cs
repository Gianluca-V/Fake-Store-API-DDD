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

        public UserId id;
        public string username { get; set; } = null!;
        public UserEmail email { get; set; } = null!;
        public string password { get; set; } = null!;

        private User() { }

        public static User Create(string Username, string Email, string Password)
        {
            return new User {
                id = UserId.CreateUnique(),
                username = Username,
                email = UserEmail.CreateEmail(Email),
                password = Password
            };
        }
    }
}
