using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Nursary_ActivityRepository : INursary_ActivityRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Nursary_ActivityRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Nursary_Activity>> GetAllNursary_Activity()
        {
            return await FarmDbContext.Nursary_Activity.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetNursary_ActivityLookup()
        {
            return await FarmDbContext.Nursary_Activity
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Nursary_Batch_Id
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Nursary_Activity> GetNursary_ActivityById(int id)
        {
            return await FarmDbContext.Nursary_Activity.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Nursary_Activity> CreateNursary_Activity(Nursary_Activity nursary_Activity)
        {
            nursary_Activity.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(nursary_Activity);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Nursary_Activity.First(a => a.Id == nursary_Activity.Id);
        }

        public async Task<Nursary_Activity> UpdateNursary_Activity(int Id, Nursary_Activity nursary_Activity)
        {
            var nursary_ActivityDetails = await FarmDbContext.Nursary_Activity.FirstOrDefaultAsync(x => x.Id == Id);
            if (nursary_ActivityDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = nursary_ActivityDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(nursary_ActivityDetails, nursary_Activity.GetType().GetProperty(propInfo.Name).GetValue(nursary_Activity));
            }
            nursary_ActivityDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Nursary_Activity.First(a => a.Id == nursary_ActivityDetails.Id);
        }

	  public async Task<IEnumerable<Nursary_Activity>> DeleteNursary_Activity(int id)
      {
            var list = await FarmDbContext.Nursary_Activity.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Nursary_Activity.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Nursary_Activity> UpdateNursary_ActivityStatus(int Id)
        {
            var nursary_ActivityDetails = await FarmDbContext.Nursary_Activity.FirstOrDefaultAsync(x => x.Id == Id);
            if (nursary_ActivityDetails == null)
            {
                return null;
            }
            nursary_ActivityDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Nursary_Activity.First(a => a.Id == nursary_ActivityDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Nursary_Activity.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchNursary_Activity(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "")
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchNursary_Activity", new SqlConnection(ConnectionString));
		adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@userId", SqlDbType = SqlDbType.VarChar, Value = userId, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@txt", SqlDbType = SqlDbType.VarChar, Value = searchString, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@pageIndex", SqlDbType = SqlDbType.VarChar, Value = pageNumber, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@pageSize", SqlDbType = SqlDbType.VarChar, Value = pageSize, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@sortColumn", SqlDbType = SqlDbType.VarChar, Value = sortColumn, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@sortDirection", SqlDbType = SqlDbType.VarChar, Value = sortDirection, Direction = ParameterDirection.Input });
		adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@isColumnSearch", SqlDbType = SqlDbType.Bit, Value = isColumnSearch, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@columnName", SqlDbType = SqlDbType.VarChar, Value = columnName, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@columnDataType", SqlDbType = SqlDbType.VarChar, Value = columnDataType, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@operator", SqlDbType = SqlDbType.VarChar, Value = operatorType, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@value1", SqlDbType = SqlDbType.VarChar, Value = value1, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@value2", SqlDbType = SqlDbType.VarChar, Value = value2, Direction = ParameterDirection.Input });

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