using PostTrades.Domain;

namespace PostTrades.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}
