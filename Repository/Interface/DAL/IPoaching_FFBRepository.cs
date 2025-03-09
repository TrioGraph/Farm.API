using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IPoaching_FFBRepository
    {
        Task<List<Poaching_FFB>> GetAllPoaching_FFB();
        
        Task<IEnumerable<object>> GetPoaching_FFBLookup();
		
        Task<Poaching_FFB> GetPoaching_FFBById(int id);

        Task<Poaching_FFB> CreatePoaching_FFB(Poaching_FFB appsPermissions);

        Task<Poaching_FFB> UpdatePoaching_FFB(int Id, Poaching_FFB role);
    
        Task<Poaching_FFB> UpdatePoaching_FFBStatus(int Id);

        Task<IEnumerable<Poaching_FFB>> DeletePoaching_FFB(int roles);
       
        Dictionary<string, object> SearchPoaching_FFB(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}