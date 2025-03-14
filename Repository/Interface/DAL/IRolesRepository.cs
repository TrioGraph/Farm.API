using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IRolesRepository
    {
        Task<List<Roles>> GetAllRoles();
        
        Task<IEnumerable<object>> GetRolesLookup();
		
        Task<Roles> GetRolesById(int id);

        Task<Roles> CreateRoles(Roles appsPermissions);

        Task<Roles> UpdateRoles(int Id, Roles role);
    
        Task<Roles> UpdateRolesStatus(int Id);

        Task<IEnumerable<Roles>> DeleteRoles(int roles);
       
        Dictionary<string, object> SearchRoles(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}