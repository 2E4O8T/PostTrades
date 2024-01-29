using PostTrades.Domain;

namespace PostTrades.Repositories
{
    public interface IRuleNameRepository
    {
        Task<IEnumerable<RuleName>> GetAllAsync();
        Task<RuleName> GetByIdAsync(int id);
        Task<int> CreateAsync(RuleName ruleName);
        Task UpdateAsync(RuleName ruleName);
        Task<int> DeleteAsync(int id);
    }
}
