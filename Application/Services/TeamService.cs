using Application.Dtos;
using Application.Dtos.Validations;
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
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
        }

        public async Task<Result<TeamDto>> GetByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            if (team == null)
                return NotFound<TeamDto>("Team not found!");

            return Ok(_mapper.Map<TeamDto>(team));
        }

        public async Task<Result<IEnumerable<TeamDto>>> GetAllAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TeamDto>>(teams));
        }

        public async Task<Result<TeamDto>> InsertAsync(TeamDto teamDto)
        {
            if (teamDto == null)
                return Fail<TeamDto>("The object cannot be null");

            var dtoValidationResult = new TeamDtoValidator().Validate(teamDto);

            if (!dtoValidationResult.IsValid)
                return RequestError<TeamDto>("Validation errors occurred!", dtoValidationResult);

            teamDto.PrimaryColor = teamDto.PrimaryColor.ToUpper();
            teamDto.SecondaryColor = teamDto.SecondaryColor.ToUpper();

            var team = _mapper.Map<Team>(teamDto);

            try
            {
                await _unitOfWork.BeginTransaction();

                var repositoryReturn = await _teamRepository.InsertAsync(team);

                await _unitOfWork.Commit();

                return Ok(_mapper.Map<TeamDto>(repositoryReturn));
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<Result<TeamDto>> UpdateAsync(TeamDto teamDto)
        {
            if (teamDto == null)
                return Fail<TeamDto>("The object cannot be null");

            var team = await _teamRepository.GetByIdAsync(teamDto.Id);

            if (team == null)
                return NotFound<TeamDto>("Team not found!");

            var dtoValidationResult = new TeamDtoValidator().Validate(teamDto);

            if (!dtoValidationResult.IsValid)
                return RequestError<TeamDto>("Validation errors occurred!", dtoValidationResult);

            teamDto.PrimaryColor = teamDto.PrimaryColor.ToUpper();
            teamDto.SecondaryColor = teamDto.SecondaryColor.ToUpper();

            team = _mapper.Map(teamDto, team);

            try
            {
                await _unitOfWork.BeginTransaction();

                var repositoryReturn = _teamRepository.Update(team);

                await _unitOfWork.Commit();

                return Ok(_mapper.Map<TeamDto>(repositoryReturn));
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
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
                await _unitOfWork.BeginTransaction();

                var repositoryReturn = _teamRepository.Delete(team);

                await _unitOfWork.Commit();

                return Ok(_mapper.Map<TeamDto>(repositoryReturn));
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
