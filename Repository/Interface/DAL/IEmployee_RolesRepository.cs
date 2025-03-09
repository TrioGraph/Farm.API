using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IEmployee_RolesRepository
    {
        Task<List<Employee_Roles>> GetAllEmployee_Roles();
        
        Task<IEnumerable<object>> GetEmployee_RolesLookup();
		
        Task<Employee_Roles> GetEmployee_RolesById(int id);

        Task<Employee_Roles> CreateEmployee_Roles(Employee_Roles appsPermissions);

        Task<Employee_Roles> UpdateEmployee_Roles(int Id, Employee_Roles role);
    
        Task<Employee_Roles> UpdateEmployee_RolesStatus(int Id);

        Task<IEnumerable<Employee_Roles>> DeleteEmployee_Roles(int roles);
       
        Dictionary<string, object> SearchEmployee_Roles(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}