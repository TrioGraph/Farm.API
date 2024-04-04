using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface ILogins_LogRepository
    {
        Task<List<Logins_Log>> GetAllLogins_Log();
        
        Task<IEnumerable<object>> GetLogins_LogLookup();
		
        Task<Logins_Log> GetLogins_LogById(int id);

        Task<Logins_Log> CreateLogins_Log(Logins_Log appsPermissions);

        Task<Logins_Log> UpdateLogins_Log(int Id, Logins_Log role);
    
        Task<Logins_Log> UpdateLogins_LogStatus(int Id);

        Task<IEnumerable<Logins_Log>> DeleteLogins_Log(int roles);
       
        Dictionary<string, object> SearchLogins_Log(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}