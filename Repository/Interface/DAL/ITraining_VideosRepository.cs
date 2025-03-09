using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface ITraining_VideosRepository
    {
        Task<List<Training_Videos>> GetAllTraining_Videos();
        
        Task<IEnumerable<object>> GetTraining_VideosLookup();
		
        Task<Training_Videos> GetTraining_VideosById(int id);

        Task<Training_Videos> CreateTraining_Videos(Training_Videos appsPermissions);

        Task<Training_Videos> UpdateTraining_Videos(int Id, Training_Videos role);
    
        Task<Training_Videos> UpdateTraining_VideosStatus(int Id);

        Task<IEnumerable<Training_Videos>> DeleteTraining_Videos(int roles);
       
        Dictionary<string, object> SearchTraining_Videos(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}