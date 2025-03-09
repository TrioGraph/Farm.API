using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IAuthorisationRepository
    {
        Task<List<Authorisation>> GetAllAuthorisation();
        
        Task<IEnumerable<object>> GetAuthorisationLookup();
		
        Task<Authorisation> GetAuthorisationById(int id);

        Task<Authorisation> CreateAuthorisation(Authorisation appsPermissions);

        Task<Authorisation> UpdateAuthorisation(int Id, Authorisation role);
    
        Task<Authorisation> UpdateAuthorisationStatus(int Id);

        Task<IEnumerable<Authorisation>> DeleteAuthorisation(int roles);
       
        Dictionary<string, object> SearchAuthorisation(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}