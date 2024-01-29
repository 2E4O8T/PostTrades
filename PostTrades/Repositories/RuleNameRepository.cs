using PostTrades.Data;
using PostTrades.Domain;
using Microsoft.EntityFrameworkCore;

namespace PostTrades.Repositories
{
    public class RuleNameRepository : IRuleNameRepository
    {
        private readonly PostTradesDbContext _postTradesContext;

        public RuleNameRepository(PostTradesDbContext postTradesDbContext)
        {
            _postTradesContext = postTradesDbContext;
        }

        public async Task<IEnumerable<RuleName>> GetAllAsync()
        {
            return await _postTradesContext.RuleNames.ToListAsync();
        }

        public async Task<RuleName> GetByIdAsync(int id)
        {
            return await _postTradesContext.RuleNames.FindAsync(id);
        }

        public async Task<int> CreateAsync(RuleName ruleName)
        {
            _postTradesContext.RuleNames.Add(ruleName);

            return await _postTradesContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(RuleName ruleName)
        {
            _postTradesContext.Entry(ruleName).State = EntityState.Modified;

            await _postTradesContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var ruleName = await _postTradesContext.RuleNames.FindAsync(id);
            _postTradesContext.RuleNames.Remove(ruleName);

            return await _postTradesContext.SaveChangesAsync();
        }
    }
}
