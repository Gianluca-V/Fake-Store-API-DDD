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

        private User(UserId Id, string Username, UserEmail Email, string Password) : base(Id)
        {
            id = Id;
            username = Username;
            email = Email;
            password = Password;
        }

        public static User Create(string Username, string Email, string Password)
        {
            return new User(
                UserId.CreateUnique(),
                Username,
                UserEmail.CreateEmail(Email),
                Password
            );
        }
    }
}
