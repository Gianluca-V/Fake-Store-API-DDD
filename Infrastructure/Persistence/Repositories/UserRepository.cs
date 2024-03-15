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
        private readonly FakeStoreAPIDbContext dbContext;

        public UserRepository(FakeStoreAPIDbContext DbContext)
        {
            dbContext = DbContext;
        }
        public UserRepository() { }

        public virtual void Add(User user)
        {
            dbContext.Users.Add(user);
        }

        public virtual User? GetUserByEmail(string email)
        {
            return dbContext.Users.SingleOrDefault(u => u.email.value == email);
        }
    }
}
