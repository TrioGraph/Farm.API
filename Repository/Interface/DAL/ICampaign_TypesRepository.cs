using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface ICampaign_TypesRepository
    {
        Task<List<Campaign_Types>> GetAllCampaign_Types();
        
        Task<IEnumerable<object>> GetCampaign_TypesLookup();
		
        Task<Campaign_Types> GetCampaign_TypesById(int id);

        Task<Campaign_Types> CreateCampaign_Types(Campaign_Types appsPermissions);

        Task<Campaign_Types> UpdateCampaign_Types(int Id, Campaign_Types role);
    
        Task<Campaign_Types> UpdateCampaign_TypesStatus(int Id);

        Task<IEnumerable<Campaign_Types>> DeleteCampaign_Types(int roles);
       
        Dictionary<string, object> SearchCampaign_Types(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}