using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IPhotosRepository
    {
        Task<List<Photos>> GetAllPhotos();
        
        Task<IEnumerable<object>> GetPhotosLookup();
		
        Task<Photos> GetPhotosById(int id);

        Task<Photos> CreatePhotos(Photos appsPermissions);

        Task<Photos> UpdatePhotos(int Id, Photos role);
    
        Task<Photos> UpdatePhotosStatus(int Id);

        Task<IEnumerable<Photos>> DeletePhotos(int roles);
       
        Dictionary<string, object> SearchPhotos(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}