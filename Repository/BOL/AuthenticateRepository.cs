using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public AuthenticateRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }
        public async Task<Employees> ValidateCredentials(string userName, string password)
        {
            return await FarmDbContext.Employees.FirstOrDefaultAsync(x => x.User_Name.Equals(userName) && x.Password.Equals(password));
        }

        public async Task<string[]> GetRole_PrivilegesByRole(int? roleId)
        {
            string[] result = string.Join(",", FarmDbContext.Role_Privileges
           .Where(a => a.Role_id == roleId)
           .Select(p => p.Privilege_Id.ToString())).Split(',');

            return result;

        }

    }
}