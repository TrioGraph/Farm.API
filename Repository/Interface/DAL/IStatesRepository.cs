using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IStatesRepository
    {
        Task<List<States>> GetAllStates();
        
        Task<IEnumerable<object>> GetStatesLookup();
		
        Task<States> GetStatesById(int id);

        Task<States> CreateStates(States appsPermissions);

        Task<States> UpdateStates(int Id, States role);
    
        Task<States> UpdateStatesStatus(int Id);

        Task<IEnumerable<States>> DeleteStates(int roles);
       
        Dictionary<string, object> SearchStates(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}