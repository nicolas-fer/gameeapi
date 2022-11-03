using Application.Dtos;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<LoginDto>> LoginAsync(LoginDto loginDto);
    }
}
