using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IEmployee_TypesRepository
    {
        Task<List<Employee_Types>> GetAllEmployee_Types();
        
        Task<IEnumerable<object>> GetEmployee_TypesLookup();
		
        Task<Employee_Types> GetEmployee_TypesById(int id);

        Task<Employee_Types> CreateEmployee_Types(Employee_Types appsPermissions);

        Task<Employee_Types> UpdateEmployee_Types(int Id, Employee_Types role);
    
        Task<Employee_Types> UpdateEmployee_TypesStatus(int Id);

        Task<IEnumerable<Employee_Types>> DeleteEmployee_Types(int roles);
       
        Dictionary<string, object> SearchEmployee_Types(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}