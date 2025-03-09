using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IReferral_SourceRepository
    {
        Task<List<Referral_Source>> GetAllReferral_Source();
        
        Task<IEnumerable<object>> GetReferral_SourceLookup();
		
        Task<Referral_Source> GetReferral_SourceById(int id);

        Task<Referral_Source> CreateReferral_Source(Referral_Source appsPermissions);

        Task<Referral_Source> UpdateReferral_Source(int Id, Referral_Source role);
    
        Task<Referral_Source> UpdateReferral_SourceStatus(int Id);

        Task<IEnumerable<Referral_Source>> DeleteReferral_Source(int roles);
       
        Dictionary<string, object> SearchReferral_Source(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}