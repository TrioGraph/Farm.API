using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IField_VisitRepository
    {
        Task<List<Field_Visit>> GetAllField_Visit();
        
        Task<IEnumerable<object>> GetField_VisitLookup();
		
        Task<Field_Visit> GetField_VisitById(int id);

        Task<Field_Visit> CreateField_Visit(Field_Visit appsPermissions);

        Task<Field_Visit> UpdateField_Visit(int Id, Field_Visit role);
    
        Task<Field_Visit> UpdateField_VisitStatus(int Id);

        Task<IEnumerable<Field_Visit>> DeleteField_Visit(int roles);
       
        Dictionary<string, object> SearchField_Visit(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}