using Domain.Interfaces.Repositories.Base;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Data.Repositories.Base
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        private readonly GameeDbContext _db;

        public Repository(GameeDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(GameeDbContext));
        }

        public async Task<TModel?> GetAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _db.Set<TModel>().FindAsync(predicate).ConfigureAwait(true);
        }

        public async Task<TModel?> GetByIdAsync(int? id)
        {
            return await _db.Set<TModel>().FindAsync(id).ConfigureAwait(true);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await _db.Set<TModel>().ToListAsync().ConfigureAwait(true);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _db.Set<TModel>().Where(predicate).ToListAsync().ConfigureAwait(true);
        }

        public async Task<TModel> InsertAsync(TModel model)
        {
            return (await _db.Set<TModel>().AddAsync(model).ConfigureAwait(true)).Entity;
        }

        public TModel Update(TModel model)
        {
            return _db.Set<TModel>().Update(model).Entity;
        }

        public TModel Delete(TModel model)
        {
            return _db.Set<TModel>().Remove(model).Entity;
        }
    }
}
