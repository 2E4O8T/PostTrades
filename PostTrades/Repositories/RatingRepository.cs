using PostTrades.Data;
using PostTrades.Domain;
using Microsoft.EntityFrameworkCore;

namespace PostTrades.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly PostTradesDbContext _postTradesContext;

        public RatingRepository(PostTradesDbContext postTradesDbContext)
        {
            _postTradesContext = postTradesDbContext;
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            return await _postTradesContext.Ratings.ToListAsync();
        }

        public async Task<Rating> GetByIdAsync(int id)
        {
            return await _postTradesContext.Ratings.FindAsync(id);
        }

        public async Task<int> CreateAsync(Rating rating)
        {
            _postTradesContext.Ratings.Add(rating);

            return await _postTradesContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rating rating)
        {
            _postTradesContext.Entry(rating).State = EntityState.Modified;

            await _postTradesContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var rating = await _postTradesContext.Ratings.FindAsync(id);
            _postTradesContext.Ratings.Remove(rating);

            return await _postTradesContext.SaveChangesAsync();
        }
    }
}
