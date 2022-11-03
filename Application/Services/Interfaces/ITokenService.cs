using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
