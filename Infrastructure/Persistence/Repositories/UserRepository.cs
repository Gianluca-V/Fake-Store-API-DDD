using Application.Common.Interfaces.Persistence;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
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

        public virtual async Task Add(User user)
        {
            await dbContext.Users.AddAsync(user);
        }

        public virtual async Task<User?> GetUserByEmail(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email.value == email);
        }
    }
}
