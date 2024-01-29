using PostTrades.Data;
using PostTrades.Domain;
using Microsoft.EntityFrameworkCore;

namespace PostTrades.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly PostTradesDbContext _postTradesContext;

        public CurvePointRepository(PostTradesDbContext postTradesDbContext)
        {
            _postTradesContext = postTradesDbContext;
        }

        public async Task<IEnumerable<CurvePoint>> GetAllAsync()
        {
            return await _postTradesContext.CurvePoints.ToListAsync();
        }

        public async Task<CurvePoint> GetByIdAsync(int id)
        {
            return await _postTradesContext.CurvePoints.FindAsync(id);
        }

        public async Task<int> CreateAsync(CurvePoint curvePoint)
        {
            _postTradesContext.CurvePoints.Add(curvePoint);

            return await _postTradesContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CurvePoint curvePoint)
        {
            _postTradesContext.Entry(curvePoint).State = EntityState.Modified;

            await _postTradesContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var curvePoint = await _postTradesContext.CurvePoints.FindAsync(id);
            _postTradesContext.CurvePoints.Remove(curvePoint);

            return await _postTradesContext.SaveChangesAsync();
        }
    }
}
