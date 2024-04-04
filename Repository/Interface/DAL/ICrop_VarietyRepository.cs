using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface ICrop_VarietyRepository
    {
        Task<List<Crop_Variety>> GetAllCrop_Variety();
        
        Task<IEnumerable<object>> GetCrop_VarietyLookup();
		
        Task<Crop_Variety> GetCrop_VarietyById(int id);

        Task<Crop_Variety> CreateCrop_Variety(Crop_Variety appsPermissions);

        Task<Crop_Variety> UpdateCrop_Variety(int Id, Crop_Variety role);
    
        Task<Crop_Variety> UpdateCrop_VarietyStatus(int Id);

        Task<IEnumerable<Crop_Variety>> DeleteCrop_Variety(int roles);
       
        Dictionary<string, object> SearchCrop_Variety(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}