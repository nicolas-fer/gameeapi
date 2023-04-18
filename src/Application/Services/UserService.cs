using Application.Dtos;
using Application.Services.Base;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public UserService(IUnitOfWork unitOfWork, 
                           IMapper mapper, 
                           IUserRepository userRepository, 
                           ITokenService tokenService) : base(unitOfWork, mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<LoginDto>> LoginAsync(LoginDto? loginDto)
        {
            if (loginDto == null)
                return Fail<LoginDto>("The body cannot be null");

            var user = await _userRepository.GetAsync(x => x.Username.Equals(loginDto.Username) && x.Password.Equals(loginDto.Password));

            if (user == null)
                return NotFound<LoginDto>("Incorrect username or password!");

            var token = _tokenService.GenerateToken(user);

            loginDto.Password = null;
            loginDto.Token = token;

            return Ok(loginDto);
        }
    }
}
