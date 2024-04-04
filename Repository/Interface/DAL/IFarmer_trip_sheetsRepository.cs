using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IFarmer_trip_sheetsRepository
    {
        Task<List<Farmer_trip_sheets>> GetAllFarmer_trip_sheets();
        
        Task<IEnumerable<object>> GetFarmer_trip_sheetsLookup();
		
        Task<Farmer_trip_sheets> GetFarmer_trip_sheetsById(int id);

        Task<Farmer_trip_sheets> CreateFarmer_trip_sheets(Farmer_trip_sheets appsPermissions);

        Task<Farmer_trip_sheets> UpdateFarmer_trip_sheets(int Id, Farmer_trip_sheets role);
    
        Task<Farmer_trip_sheets> UpdateFarmer_trip_sheetsStatus(int Id);

        Task<IEnumerable<Farmer_trip_sheets>> DeleteFarmer_trip_sheets(int roles);
       
        Dictionary<string, object> SearchFarmer_trip_sheets(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}