using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Logins_LogRepository : ILogins_LogRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Logins_LogRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Logins_Log>> GetAllLogins_Log()
        {
            return await FarmDbContext.Logins_Log.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetLogins_LogLookup()
        {
            return await FarmDbContext.Logins_Log
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Login_Id
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Logins_Log> GetLogins_LogById(int id)
        {
            return await FarmDbContext.Logins_Log.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Logins_Log> CreateLogins_Log(Logins_Log logins_Log)
        {
            logins_Log.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(logins_Log);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Logins_Log.First(a => a.Id == logins_Log.Id);
        }

        public async Task<Logins_Log> UpdateLogins_Log(int Id, Logins_Log logins_Log)
        {
            var logins_LogDetails = await FarmDbContext.Logins_Log.FirstOrDefaultAsync(x => x.Id == Id);
            if (logins_LogDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = logins_LogDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(logins_LogDetails, logins_Log.GetType().GetProperty(propInfo.Name).GetValue(logins_Log));
            }
            logins_LogDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Logins_Log.First(a => a.Id == logins_LogDetails.Id);
        }

	  public async Task<IEnumerable<Logins_Log>> DeleteLogins_Log(int id)
      {
            var list = await FarmDbContext.Logins_Log.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Logins_Log.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Logins_Log> UpdateLogins_LogStatus(int Id)
        {
            var logins_LogDetails = await FarmDbContext.Logins_Log.FirstOrDefaultAsync(x => x.Id == Id);
            if (logins_LogDetails == null)
            {
                return null;
            }
            // logins_LogDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Logins_Log.First(a => a.Id == logins_LogDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Logins_Log.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchLogins_Log(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchLogins_Log", new SqlConnection(ConnectionString));
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