using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IEmployeesRepository
    {
        Task<List<Employees>> GetAllEmployees();
        
        Task<IEnumerable<object>> GetEmployeesLookup();
		
        Task<Employees> GetEmployeesById(int id);

        Task<Employees> CreateEmployees(Employees appsPermissions);

        Task<Employees> UpdateEmployees(int Id, Employees role);
    
        Task<Employees> UpdateEmployeesStatus(int Id);

        Task<IEnumerable<Employees>> DeleteEmployees(int roles);
       
        Dictionary<string, object> SearchEmployees(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}