using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IAuthenticateRepository
    {
        Task<Employees> ValidateCredentials(string userName, string password);
        Task<string[]> GetRole_PrivilegesByRole(int? roleId);

    }
}