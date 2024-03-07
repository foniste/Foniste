using FonApi.Database;
using FonApi.Models.Accounts;

namespace FonApi.Interfaces{
    public interface IAccountDbService{
        Task<List<Users>> GetAllUsers();
        Task<List<LoginLog>> GetAllLog();
        Task<List<Organizations>> GetAllOrganization();
        Task<List<Role>> GetAllRole();
        int GetUserIdByEmail(string email,string password);
        bool IsExistsInUserDb(string email);
    }
}