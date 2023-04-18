using Application.Dtos;
using Application.Dtos.Validations;
using Application.Services.Base;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Application.Services
{
    public class TeamService : BaseService, ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(IUnitOfWork unitOfWork, IMapper mapper, ITeamRepository teamRepository) : base(unitOfWork, mapper)
        {
            _teamRepository = teamRepository;
        }

        public async Task<Result<TeamDto>> GetByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            if (team == null)
                return NotFound<TeamDto>("Team not found!");

            return Ok(Mapper.Map<TeamDto>(team));
        }

        public async Task<Result<IEnumerable<TeamDto>>> GetAllAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            return Ok(Mapper.Map<IEnumerable<TeamDto>>(teams));
        }

        public async Task<Result<TeamDto>> InsertAsync(TeamDto? teamDto)
        {
            if (teamDto == null)
                return Fail<TeamDto>("The body cannot be null");

            var dtoValidationResult = await new TeamDtoValidator().ValidateAsync(teamDto);

            if (!dtoValidationResult.IsValid)
                return RequestError<TeamDto>("Validation errors occurred!", dtoValidationResult);

            teamDto.PrimaryColor = teamDto.PrimaryColor.ToUpper();
            teamDto.SecondaryColor = teamDto.SecondaryColor.ToUpper();

            var team = Mapper.Map<Team>(teamDto);

            try
            {
                await UnitOfWork.BeginTransaction();

                var repositoryReturn = await _teamRepository.InsertAsync(team);

                await UnitOfWork.Commit();

                return Ok(Mapper.Map<TeamDto>(repositoryReturn));
            }
            catch (Exception)
            {
                await UnitOfWork.Rollback();
                throw;
            }
        }

        public async Task<Result<TeamDto>> UpdateAsync(TeamDto? teamDto)
        {
            if (teamDto == null)
                return Fail<TeamDto>("The object cannot be null");

            var team = await _teamRepository.GetByIdAsync(teamDto.Id);

            if (team == null)
                return NotFound<TeamDto>("Team not found!");

            var dtoValidationResult = await new TeamDtoValidator().ValidateAsync(teamDto);

            if (!dtoValidationResult.IsValid)
                return RequestError<TeamDto>("Validation errors occurred!", dtoValidationResult);

            teamDto.PrimaryColor = teamDto.PrimaryColor.ToUpper();
            teamDto.SecondaryColor = teamDto.SecondaryColor.ToUpper();

            team = Mapper.Map(teamDto, team);

            try
            {
                await UnitOfWork.BeginTransaction();

                var repositoryReturn = _teamRepository.Update(team);

                await UnitOfWork.Commit();

                return Ok(Mapper.Map<TeamDto>(repositoryReturn));
            }
            catch (Exception)
            {
                await UnitOfWork.Rollback();
                throw;
            }
        }

        public async Task<Result<TeamDto>> DeleteAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            if (team == null)
                return NotFound<TeamDto>("Team not found!");

            try
            {
                await UnitOfWork.BeginTransaction();

                var repositoryReturn = _teamRepository.Delete(team);

                await UnitOfWork.Commit();

                return Ok(Mapper.Map<TeamDto>(repositoryReturn));
            }
            catch (Exception)
            {
                await UnitOfWork.Rollback();
                throw;
            }
        }
    }
}
