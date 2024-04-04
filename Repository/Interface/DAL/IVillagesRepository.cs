using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IVillagesRepository
    {
        Task<List<Villages>> GetAllVillages();
        
        Task<IEnumerable<object>> GetVillagesLookup();
		
        Task<Villages> GetVillagesById(int id);

        Task<Villages> CreateVillages(Villages appsPermissions);

        Task<Villages> UpdateVillages(int Id, Villages role);
    
        Task<Villages> UpdateVillagesStatus(int Id);

        Task<IEnumerable<Villages>> DeleteVillages(int roles);
       
        Dictionary<string, object> SearchVillages(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}