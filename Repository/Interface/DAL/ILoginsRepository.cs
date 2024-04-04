using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface ILoginsRepository
    {
        Task<List<Logins>> GetAllLogins();
        
        Task<IEnumerable<object>> GetLoginsLookup();
		
        Task<Logins> GetLoginsById(int id);

        Task<Logins> CreateLogins(Logins appsPermissions);

        Task<Logins> UpdateLogins(int Id, Logins role);
    
        Task<Logins> UpdateLoginsStatus(int Id);

        Task<IEnumerable<Logins>> DeleteLogins(int roles);
       
        Dictionary<string, object> SearchLogins(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}