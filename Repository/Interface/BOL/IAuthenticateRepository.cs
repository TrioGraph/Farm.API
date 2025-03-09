using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IAuthenticateRepository
    {
        Task<Users> ValidateCredentials(string userName, string password);
        Task<string[]> GetRole_PrivilegesByRole(int? roleId);

        Task<object> GetLookupRecentUpdates();


    }
}