using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IFarmers_LoginRepository
    {
        Task<List<Farmers_Login>> GetAllFarmers_Login();
        
        Task<IEnumerable<object>> GetFarmers_LoginLookup();
		
        Task<Farmers_Login> GetFarmers_LoginById(int id);

        Task<Farmers_Login> CreateFarmers_Login(Farmers_Login appsPermissions);

        Task<Farmers_Login> UpdateFarmers_Login(int Id, Farmers_Login role);
    
        Task<Farmers_Login> UpdateFarmers_LoginStatus(int Id);

        Task<IEnumerable<Farmers_Login>> DeleteFarmers_Login(int roles);
       
        Dictionary<string, object> SearchFarmers_Login(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}