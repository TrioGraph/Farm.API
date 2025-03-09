using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IDocument_TypeRepository
    {
        Task<List<Document_Type>> GetAllDocument_Type();
        
        Task<IEnumerable<object>> GetDocument_TypeLookup();
		
        Task<Document_Type> GetDocument_TypeById(int id);

        Task<Document_Type> CreateDocument_Type(Document_Type appsPermissions);

        Task<Document_Type> UpdateDocument_Type(int Id, Document_Type role);
    
        Task<Document_Type> UpdateDocument_TypeStatus(int Id);

        Task<IEnumerable<Document_Type>> DeleteDocument_Type(int roles);
       
        Dictionary<string, object> SearchDocument_Type(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}