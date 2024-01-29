using PostTrades.Domain;

namespace PostTrades.Repositories
{
    public interface ITradeRepository
    {
        Task<IEnumerable<Trade>> GetAllAsync();
        Task<Trade> GetByIdAsync(int id);
        Task<int> CreateAsync(Trade trade);
        Task UpdateAsync(Trade trade);
        Task<int> DeleteAsync(int id);
    }
}
