using PostTrades.Data;
using PostTrades.Domain;
using Microsoft.EntityFrameworkCore;

namespace PostTrades.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly PostTradesDbContext _postTradesContext;

        public TradeRepository(PostTradesDbContext postTradesDbContext)
        {
            _postTradesContext = postTradesDbContext;
        }

        public async Task<IEnumerable<Trade>> GetAllAsync()
        {
            return await _postTradesContext.Trades.ToListAsync();
        }

        public async Task<Trade> GetByIdAsync(int id)
        {
            return await _postTradesContext.Trades.FindAsync(id);
        }

        public async Task<int> CreateAsync(Trade trade)
        {
            _postTradesContext.Trades.Add(trade);

            return await _postTradesContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Trade trade)
        {
            _postTradesContext.Entry(trade).State = EntityState.Modified;

            await _postTradesContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var trade = await _postTradesContext.Trades.FindAsync(id);
            _postTradesContext.Trades.Remove(trade);

            return await _postTradesContext.SaveChangesAsync();
        }
    }
}
