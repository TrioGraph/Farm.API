using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IDocumentsRepository
    {
        Task<List<Documents>> GetAllDocuments();
        
        Task<IEnumerable<object>> GetDocumentsLookup();
		
        Task<Documents> GetDocumentsById(int id);

        Task<Documents> CreateDocuments(Documents appsPermissions);

        Task<Documents> UpdateDocuments(int Id, Documents role);
    
        Task<Documents> UpdateDocumentsStatus(int Id);

        Task<IEnumerable<Documents>> DeleteDocuments(int roles);
       
        Dictionary<string, object> SearchDocuments(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}