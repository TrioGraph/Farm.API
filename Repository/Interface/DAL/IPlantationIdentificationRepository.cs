using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IPlantationIdentificationRepository
    {
        Task<List<PlantationIdentification>> GetAllPlantationIdentification();
        
        Task<IEnumerable<object>> GetPlantationIdentificationLookup();
		
        Task<PlantationIdentification> GetPlantationIdentificationById(int id);

        Task<PlantationIdentification> CreatePlantationIdentification(PlantationIdentification appsPermissions);

        Task<PlantationIdentification> UpdatePlantationIdentification(int Id, PlantationIdentification role);
    
        Task<PlantationIdentification> UpdatePlantationIdentificationStatus(int Id);

        Task<IEnumerable<PlantationIdentification>> DeletePlantationIdentification(int roles);
       
        Dictionary<string, object> SearchPlantationIdentification(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection,
        bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}