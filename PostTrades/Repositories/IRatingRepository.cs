using PostTrades.Domain;

namespace PostTrades.Repositories
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating> GetByIdAsync(int id);
        Task<int> CreateAsync(Rating rating);
        Task UpdateAsync(Rating rating);
        Task<int> DeleteAsync(int id);
    }
}
