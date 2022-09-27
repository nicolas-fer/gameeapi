using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Data.Context;
using Infra.Data.Repositories.Base;

namespace Infra.Data.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(GameeDbContext db) : base(db)
        {
        }
    }
}
