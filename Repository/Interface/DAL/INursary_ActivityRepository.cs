using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface INursary_ActivityRepository
    {
        Task<List<Nursary_Activity>> GetAllNursary_Activity();
        
        Task<IEnumerable<object>> GetNursary_ActivityLookup();
		
        Task<Nursary_Activity> GetNursary_ActivityById(int id);

        Task<Nursary_Activity> CreateNursary_Activity(Nursary_Activity appsPermissions);

        Task<Nursary_Activity> UpdateNursary_Activity(int Id, Nursary_Activity role);
    
        Task<Nursary_Activity> UpdateNursary_ActivityStatus(int Id);

        Task<IEnumerable<Nursary_Activity>> DeleteNursary_Activity(int roles);
       
        Dictionary<string, object> SearchNursary_Activity(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}