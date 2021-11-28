using Data.Entities;

namespace Domain.Services.Abstractions
{
    public interface IJwtService
    {
        Task<string> CreateJwtToken(User user);
    }
}
