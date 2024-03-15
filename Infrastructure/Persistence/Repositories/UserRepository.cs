using Application.Common.Interfaces.Persistence;
using Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> users = new();
        public virtual void Add(User user)
        {
            users.Add(user);
        }

        public virtual User? GetUserByEmail(string email)
        {
            return users.SingleOrDefault(u => u.email.value == email);
        }
    }
}
