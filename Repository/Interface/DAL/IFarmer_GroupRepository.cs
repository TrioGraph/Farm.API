using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IFarmer_GroupRepository
    {
        Task<List<Farmer_Group>> GetAllFarmer_Group();
        
        Task<IEnumerable<object>> GetFarmer_GroupLookup();
		
        Task<Farmer_Group> GetFarmer_GroupById(int id);

        Task<Farmer_Group> CreateFarmer_Group(Farmer_Group appsPermissions);

        Task<Farmer_Group> UpdateFarmer_Group(int Id, Farmer_Group role);
    
        Task<Farmer_Group> UpdateFarmer_GroupStatus(int Id);

        Task<IEnumerable<Farmer_Group>> DeleteFarmer_Group(int roles);
       
        Dictionary<string, object> SearchFarmer_Group(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}