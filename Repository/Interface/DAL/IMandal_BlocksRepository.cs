using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IMandal_BlocksRepository
    {
        Task<List<Mandal_Blocks>> GetAllMandal_Blocks();
        
        Task<IEnumerable<object>> GetMandal_BlocksLookup();
		
        Task<Mandal_Blocks> GetMandal_BlocksById(int id);

        Task<Mandal_Blocks> CreateMandal_Blocks(Mandal_Blocks appsPermissions);

        Task<Mandal_Blocks> UpdateMandal_Blocks(int Id, Mandal_Blocks role);
    
        Task<Mandal_Blocks> UpdateMandal_BlocksStatus(int Id);

        Task<IEnumerable<Mandal_Blocks>> DeleteMandal_Blocks(int roles);
       
        Dictionary<string, object> SearchMandal_Blocks(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}