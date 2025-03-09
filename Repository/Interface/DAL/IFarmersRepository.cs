using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IFarmersRepository
    {
        Task<List<Farmers>> GetAllFarmers();
        
        Task<IEnumerable<object>> GetFarmersLookup();
		
        Task<Farmers> GetFarmersById(int id);

        Task<Farmers> CreateFarmers(Farmers appsPermissions);

        Task<Farmers> UpdateFarmers(int Id, Farmers role);
    
        Task<Farmers> UpdateFarmersStatus(int Id);

        Task<IEnumerable<Farmers>> DeleteFarmers(int roles);
       
        Dictionary<string, object> SearchFarmers(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}