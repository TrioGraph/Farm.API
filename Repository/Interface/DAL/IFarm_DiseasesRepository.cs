using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IFarm_DiseasesRepository
    {
        Task<List<Farm_Diseases>> GetAllFarm_Diseases();
        
        Task<IEnumerable<object>> GetFarm_DiseasesLookup();
		
        Task<Farm_Diseases> GetFarm_DiseasesById(int id);

        Task<Farm_Diseases> CreateFarm_Diseases(Farm_Diseases appsPermissions);

        Task<Farm_Diseases> UpdateFarm_Diseases(int Id, Farm_Diseases role);
    
        Task<Farm_Diseases> UpdateFarm_DiseasesStatus(int Id);

        Task<IEnumerable<Farm_Diseases>> DeleteFarm_Diseases(int roles);
       
        Dictionary<string, object> SearchFarm_Diseases(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}