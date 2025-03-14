using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IFarmFieldRepository
    {
        Task<List<FarmField>> GetAllFarmField();
        
        Task<IEnumerable<object>> GetFarmFieldLookup();
		
        Task<FarmField> GetFarmFieldById(int id);

        Task<FarmField> CreateFarmField(FarmField appsPermissions);

        Task<FarmField> UpdateFarmField(int Id, FarmField role);
    
        Task<FarmField> UpdateFarmFieldStatus(int Id);

        Task<IEnumerable<FarmField>> DeleteFarmField(int roles);
       
        Dictionary<string, object> SearchFarmField(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}