using PostTrades.Domain;

namespace PostTrades.Repositories
{
    public interface ICurvePointRepository
    {
        Task<IEnumerable<CurvePoint>> GetAllAsync();
        Task<CurvePoint> GetByIdAsync(int id);
        Task<int> CreateAsync(CurvePoint curvePoint);
        Task UpdateAsync(CurvePoint curvePoint);
        Task<int> DeleteAsync(int id);
    }
}
