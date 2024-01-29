using PostTrades.Domain;

namespace PostTrades.Repositories
{
    public interface IBidRepository
    {
        Task<IEnumerable<Bid>> GetAllAsync();
        Task<Bid> GetByIdAsync(int id);
        Task<int> CreateAsync(Bid bid);
        Task UpdateAsync(Bid bid);
        Task<int> DeleteAsync(int id);
    }
}
