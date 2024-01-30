using PostTrades.Data;
using PostTrades.Models;
using Microsoft.EntityFrameworkCore;
using PostTrades.Domain;
using PostTrades.Repositories;
using System;

namespace JwtWebApiTutorial.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PostTradesDbContext _postTradesDbContext;

        public UserRepository(PostTradesDbContext appDbContext)
        {
            _postTradesDbContext = appDbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _postTradesDbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _postTradesDbContext.Users.FindAsync(id);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var user = await _postTradesDbContext.Users.FindAsync(id);
            _postTradesDbContext.Users.Remove(user);
            return await _postTradesDbContext.SaveChangesAsync();
        }
    }
}
