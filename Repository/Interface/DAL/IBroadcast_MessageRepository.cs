using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IBroadcast_MessageRepository
    {
        Task<List<Broadcast_Message>> GetAllBroadcast_Message();
        
        Task<IEnumerable<object>> GetBroadcast_MessageLookup();
		
        Task<Broadcast_Message> GetBroadcast_MessageById(int id);

        Task<Broadcast_Message> CreateBroadcast_Message(Broadcast_Message appsPermissions);

        Task<Broadcast_Message> UpdateBroadcast_Message(int Id, Broadcast_Message role);
    
        Task<Broadcast_Message> UpdateBroadcast_MessageStatus(int Id);

        Task<IEnumerable<Broadcast_Message>> DeleteBroadcast_Message(int roles);
       
        Dictionary<string, object> SearchBroadcast_Message(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}