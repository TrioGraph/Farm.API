using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IRole_PrivilegesRepository
    {
        Task<List<Role_Privileges>> GetAllRole_Privileges();
        
        Task<IEnumerable<object>> GetRole_PrivilegesLookup();
		
        Task<Role_Privileges> GetRole_PrivilegesById(int id);

        Task<Role_Privileges> CreateRole_Privileges(Role_Privileges appsPermissions);

        Task<Role_Privileges> UpdateRole_Privileges(int Id, Role_Privileges role);
    
        Task<Role_Privileges> UpdateRole_PrivilegesStatus(int Id);

        Task<IEnumerable<Role_Privileges>> DeleteRole_Privileges(int roles);
       
        Dictionary<string, object> SearchRole_Privileges(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}