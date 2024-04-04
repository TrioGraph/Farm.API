using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface INursaryRepository
    {
        Task<List<Nursary>> GetAllNursary();
        
        Task<IEnumerable<object>> GetNursaryLookup();
		
        Task<Nursary> GetNursaryById(int id);

        Task<Nursary> CreateNursary(Nursary appsPermissions);

        Task<Nursary> UpdateNursary(int Id, Nursary role);
    
        Task<Nursary> UpdateNursaryStatus(int Id);

        Task<IEnumerable<Nursary>> DeleteNursary(int roles);
       
        Dictionary<string, object> SearchNursary(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}