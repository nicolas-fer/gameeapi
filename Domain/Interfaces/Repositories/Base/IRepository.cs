using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories.Base
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<TModel?> GetAsync(Expression<Func<TModel, bool>> predicate);
        Task<TModel?> GetByIdAsync(int? id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> predicate);
        Task<TModel> InsertAsync(TModel model);
        TModel Update(TModel model);
        TModel Delete(TModel model);
    }
}
