using Application.Dtos;

namespace Application.Services.Interfaces
{
    public interface ITeamService
    {
        Task<Result<TeamDto>> GetByIdAsync(int id);
        Task<Result<IEnumerable<TeamDto>>> GetAllAsync();
        Task<Result<TeamDto>> InsertAsync(TeamDto teamDto);
        Task<Result<TeamDto>> UpdateAsync(TeamDto teamDto);
        Task<Result<TeamDto>> DeleteAsync(int id);
    }
}
