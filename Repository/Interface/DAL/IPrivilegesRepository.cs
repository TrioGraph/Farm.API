using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IPrivilegesRepository
    {
        Task<List<Privileges>> GetAllPrivileges();
        
        Task<IEnumerable<object>> GetPrivilegesLookup();
		
        Task<Privileges> GetPrivilegesById(int id);

        Task<Privileges> CreatePrivileges(Privileges appsPermissions);

        Task<Privileges> UpdatePrivileges(int Id, Privileges role);
    
        Task<Privileges> UpdatePrivilegesStatus(int Id);

        Task<IEnumerable<Privileges>> DeletePrivileges(int roles);
       
        Dictionary<string, object> SearchPrivileges(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}