using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Data.Context;
using Infra.Data.Repositories.Base;

namespace Infra.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(GameeDbContext db) : base(db)
        {
        }
    }
}
