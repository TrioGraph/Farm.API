using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface ICampaignsRepository
    {
        Task<List<Campaigns>> GetAllCampaigns();
        
        Task<IEnumerable<object>> GetCampaignsLookup();
		
        Task<Campaigns> GetCampaignsById(int id);

        Task<Campaigns> CreateCampaigns(Campaigns appsPermissions);

        Task<Campaigns> UpdateCampaigns(int Id, Campaigns role);
    
        Task<Campaigns> UpdateCampaignsStatus(int Id);

        Task<IEnumerable<Campaigns>> DeleteCampaigns(int roles);
       
        Dictionary<string, object> SearchCampaigns(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}