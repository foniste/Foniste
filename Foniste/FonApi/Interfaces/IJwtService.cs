using FonApi.Models.Accounts;

namespace FonApi.Interfaces{
    public interface IJwtService {
        string GenerateToken(int userId, string email);
        object GenerateToken(UserAuth user);
    }
}