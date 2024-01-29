using PostTrades.Data;
using PostTrades.Domain;
using Microsoft.EntityFrameworkCore;

namespace PostTrades.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly PostTradesDbContext _postTradesContext;

        public BidRepository(PostTradesDbContext postTradesDbContext)
        {
            _postTradesContext = postTradesDbContext;
        }

        public async Task<IEnumerable<Bid>> GetAllAsync()
        {
            return await _postTradesContext.Bids.ToListAsync();
        }

        public async Task<Bid> GetByIdAsync(int id)
        {
            return await _postTradesContext.Bids.FindAsync(id);
        }

        public async Task<int> CreateAsync(Bid bid)
        {
            _postTradesContext.Bids.Add(bid);
            
            return await _postTradesContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Bid bid)
        {
            _postTradesContext.Entry(bid).State = EntityState.Modified;

            await _postTradesContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var bid = await _postTradesContext.Bids.FindAsync(id);
            _postTradesContext.Bids.Remove(bid);

            return await _postTradesContext.SaveChangesAsync();
        }
    }
}
