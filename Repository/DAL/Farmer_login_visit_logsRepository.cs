using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Farmer_login_visit_logsRepository : IFarmer_login_visit_logsRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Farmer_login_visit_logsRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Farmer_login_visit_logs>> GetAllFarmer_login_visit_logs()
        {
            return await FarmDbContext.Farmer_login_visit_logs.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetFarmer_login_visit_logsLookup()
        {
            return await FarmDbContext.Farmer_login_visit_logs
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Farmer_Tally_Code
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Farmer_login_visit_logs> GetFarmer_login_visit_logsById(int id)
        {
            return await FarmDbContext.Farmer_login_visit_logs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Farmer_login_visit_logs> CreateFarmer_login_visit_logs(Farmer_login_visit_logs farmer_login_visit_logs)
        {
            farmer_login_visit_logs.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(farmer_login_visit_logs);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Farmer_login_visit_logs.First(a => a.Id == farmer_login_visit_logs.Id);
        }

        public async Task<Farmer_login_visit_logs> UpdateFarmer_login_visit_logs(int Id, Farmer_login_visit_logs farmer_login_visit_logs)
        {
            var farmer_login_visit_logsDetails = await FarmDbContext.Farmer_login_visit_logs.FirstOrDefaultAsync(x => x.Id == Id);
            if (farmer_login_visit_logsDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = farmer_login_visit_logsDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(farmer_login_visit_logsDetails, farmer_login_visit_logs.GetType().GetProperty(propInfo.Name).GetValue(farmer_login_visit_logs));
            }
            farmer_login_visit_logsDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Farmer_login_visit_logs.First(a => a.Id == farmer_login_visit_logsDetails.Id);
        }

	  public async Task<IEnumerable<Farmer_login_visit_logs>> DeleteFarmer_login_visit_logs(int id)
      {
            var list = await FarmDbContext.Farmer_login_visit_logs.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Farmer_login_visit_logs.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Farmer_login_visit_logs> UpdateFarmer_login_visit_logsStatus(int Id)
        {
            var farmer_login_visit_logsDetails = await FarmDbContext.Farmer_login_visit_logs.FirstOrDefaultAsync(x => x.Id == Id);
            if (farmer_login_visit_logsDetails == null)
            {
                return null;
            }
            farmer_login_visit_logsDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Farmer_login_visit_logs.First(a => a.Id == farmer_login_visit_logsDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Farmer_login_visit_logs.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchFarmer_login_visit_logs(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchFarmer_login_visit_logs", new SqlConnection(ConnectionString));
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