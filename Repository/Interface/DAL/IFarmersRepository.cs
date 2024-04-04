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
       
        Dictionary<string, object> SearchFarmers(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}