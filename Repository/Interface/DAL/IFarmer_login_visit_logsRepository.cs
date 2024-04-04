using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IFarmer_login_visit_logsRepository
    {
        Task<List<Farmer_login_visit_logs>> GetAllFarmer_login_visit_logs();
        
        Task<IEnumerable<object>> GetFarmer_login_visit_logsLookup();
		
        Task<Farmer_login_visit_logs> GetFarmer_login_visit_logsById(int id);

        Task<Farmer_login_visit_logs> CreateFarmer_login_visit_logs(Farmer_login_visit_logs appsPermissions);

        Task<Farmer_login_visit_logs> UpdateFarmer_login_visit_logs(int Id, Farmer_login_visit_logs role);
    
        Task<Farmer_login_visit_logs> UpdateFarmer_login_visit_logsStatus(int Id);

        Task<IEnumerable<Farmer_login_visit_logs>> DeleteFarmer_login_visit_logs(int roles);
       
        Dictionary<string, object> SearchFarmer_login_visit_logs(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}