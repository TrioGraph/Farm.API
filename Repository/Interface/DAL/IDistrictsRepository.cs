using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IDistrictsRepository
    {
        Task<List<Districts>> GetAllDistricts();
        
        Task<IEnumerable<object>> GetDistrictsLookup();
		
        Task<Districts> GetDistrictsById(int id);

        Task<Districts> CreateDistricts(Districts appsPermissions);

        Task<Districts> UpdateDistricts(int Id, Districts role);
    
        Task<Districts> UpdateDistrictsStatus(int Id);

        Task<IEnumerable<Districts>> DeleteDistricts(int roles);
       
        Dictionary<string, object> SearchDistricts(int userId, SearchFields searchFields);

    }
}