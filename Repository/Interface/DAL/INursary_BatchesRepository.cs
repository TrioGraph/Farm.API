using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface INursary_BatchesRepository
    {
        Task<List<Nursary_Batches>> GetAllNursary_Batches();
        
        Task<IEnumerable<object>> GetNursary_BatchesLookup();
		
        Task<Nursary_Batches> GetNursary_BatchesById(int id);

        Task<Nursary_Batches> CreateNursary_Batches(Nursary_Batches appsPermissions);

        Task<Nursary_Batches> UpdateNursary_Batches(int Id, Nursary_Batches role);
    
        Task<Nursary_Batches> UpdateNursary_BatchesStatus(int Id);

        Task<IEnumerable<Nursary_Batches>> DeleteNursary_Batches(int roles);
       
        Dictionary<string, object> SearchNursary_Batches(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}