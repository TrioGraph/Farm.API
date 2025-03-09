using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IGenderRepository
    {
        Task<List<Gender>> GetAllGender();
        
        Task<IEnumerable<object>> GetGenderLookup();
		
        Task<Gender> GetGenderById(int id);

        Task<Gender> CreateGender(Gender appsPermissions);

        Task<Gender> UpdateGender(int Id, Gender role);
    
        Task<Gender> UpdateGenderStatus(int Id);

        Task<IEnumerable<Gender>> DeleteGender(int roles);
       
        Dictionary<string, object> SearchGender(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}