using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class FarmersRepository : IFarmersRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public FarmersRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Farmers>> GetAllFarmers()
        {
            return await FarmDbContext.Farmers.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetFarmersLookup(string searchText)
        {
            if(searchText == null || (searchText != null && searchText.Equals("null")))
            {
            return await FarmDbContext.Farmers
            .Select(s => new
            {
                Id = s.Id,
                Name = s.First_Name + " " + s.Last_Name
            }).Take(50).OrderByDescending(d => d.Id).ToListAsync();
            }
            else {
            return await FarmDbContext.Farmers
            .Where(a => a.First_Name.Contains(searchText) || 
                     a.Last_Name.Contains(searchText))
                     .Select(s => new
            {
                Id = s.Id,
                Name = s.First_Name + " " + s.Last_Name
            }).Take(50).OrderByDescending(d => d.Id).ToListAsync();
            }
        }

        public async Task<Farmers> GetFarmersById(int id)
        {
            return await FarmDbContext.Farmers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Farmers> CreateFarmers(Farmers farmers)
        {
            farmers.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(farmers);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Farmers.First(a => a.Id == farmers.Id);
        }

        public async Task<Farmers> UpdateFarmers(int Id, Farmers farmers)
        {
            var farmersDetails = await FarmDbContext.Farmers.FirstOrDefaultAsync(x => x.Id == Id);
            if (farmersDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = farmersDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(farmersDetails, farmers.GetType().GetProperty(propInfo.Name).GetValue(farmers));
            }
            farmersDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Farmers.First(a => a.Id == farmersDetails.Id);
        }

	  public async Task<IEnumerable<Farmers>> DeleteFarmers(int id)
      {
            var list = await FarmDbContext.Farmers.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Farmers.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Farmers> UpdateFarmersStatus(int Id)
        {
            var farmersDetails = await FarmDbContext.Farmers.FirstOrDefaultAsync(x => x.Id == Id);
            if (farmersDetails == null)
            {
                return null;
            }
            farmersDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Farmers.First(a => a.Id == farmersDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Farmers.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchFarmers(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchFarmers", new SqlConnection(ConnectionString));
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