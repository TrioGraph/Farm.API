using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Farmer_trip_sheetsRepository : IFarmer_trip_sheetsRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Farmer_trip_sheetsRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Farmer_trip_sheets>> GetAllFarmer_trip_sheets()
        {
            return await FarmDbContext.Farmer_trip_sheets.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetFarmer_trip_sheetsLookup()
        {
            return await FarmDbContext.Farmer_trip_sheets
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Id
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Farmer_trip_sheets> GetFarmer_trip_sheetsById(int id)
        {
            return await FarmDbContext.Farmer_trip_sheets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Farmer_trip_sheets> CreateFarmer_trip_sheets(Farmer_trip_sheets farmer_trip_sheets)
        {
            farmer_trip_sheets.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(farmer_trip_sheets);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Farmer_trip_sheets.First(a => a.Id == farmer_trip_sheets.Id);
        }

        public async Task<Farmer_trip_sheets> UpdateFarmer_trip_sheets(int Id, Farmer_trip_sheets farmer_trip_sheets)
        {
            var farmer_trip_sheetsDetails = await FarmDbContext.Farmer_trip_sheets.FirstOrDefaultAsync(x => x.Id == Id);
            if (farmer_trip_sheetsDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = farmer_trip_sheetsDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(farmer_trip_sheetsDetails, farmer_trip_sheets.GetType().GetProperty(propInfo.Name).GetValue(farmer_trip_sheets));
            }
            farmer_trip_sheetsDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Farmer_trip_sheets.First(a => a.Id == farmer_trip_sheetsDetails.Id);
        }

	  public async Task<IEnumerable<Farmer_trip_sheets>> DeleteFarmer_trip_sheets(int id)
      {
            var list = await FarmDbContext.Farmer_trip_sheets.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Farmer_trip_sheets.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Farmer_trip_sheets> UpdateFarmer_trip_sheetsStatus(int Id)
        {
            var farmer_trip_sheetsDetails = await FarmDbContext.Farmer_trip_sheets.FirstOrDefaultAsync(x => x.Id == Id);
            if (farmer_trip_sheetsDetails == null)
            {
                return null;
            }
            farmer_trip_sheetsDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Farmer_trip_sheets.First(a => a.Id == farmer_trip_sheetsDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Farmer_trip_sheets.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchFarmer_trip_sheets(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchFarmer_trip_sheets", new SqlConnection(ConnectionString));
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@txt", SqlDbType = SqlDbType.VarChar, Value = searchString, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@pageIndex", SqlDbType = SqlDbType.VarChar, Value = pageNumber, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@pageSize", SqlDbType = SqlDbType.VarChar, Value = pageSize, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@sortColumn", SqlDbType = SqlDbType.VarChar, Value = sortColumn, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@sortDirection", SqlDbType = SqlDbType.VarChar, Value = sortDirection, Direction = ParameterDirection.Input });

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataset);
                Dictionary<string, string> tempRecord = new Dictionary<string, string>();
                List<object> record = new List<object>();
                string propertyName = "";
                // record = new KeyValuePair<string, string>();
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    tempRecord = new Dictionary<string, string>();
                    var obj1 = new ExpandoObject();
                    foreach (DataColumn dc in dataset.Tables[0].Columns)
                    {
                        propertyName = dc.ColumnName;
                        tempRecord.Add(dc.ColumnName, dr[dc.ColumnName] == null ? "" : Convert.ToString(dr[dc.ColumnName]));
                    }
                    record.Add(tempRecord);
                }
                result.Add("data", record);
                result.Add("TotalRecordsCount", dataset.Tables[1].Rows[0]["TotalRowCount"]);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }

            return result;
        }
}
}