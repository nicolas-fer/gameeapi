using Domain.Interfaces.Repositories;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameeDbContext _db;
        private IDbContextTransaction _transaction;

        public UnitOfWork(GameeDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(GameeDbContext));
        }

        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(true);
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync().ConfigureAwait(true);
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync().ConfigureAwait(true);
        }

        public void Dispose()
        {            
            _transaction?.Dispose();
        }
    }
}