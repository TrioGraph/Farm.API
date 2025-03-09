using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Training_VideosRepository : ITraining_VideosRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Training_VideosRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Training_Videos>> GetAllTraining_Videos()
        {
            return await FarmDbContext.Training_Videos.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetTraining_VideosLookup()
        {
            return await FarmDbContext.Training_Videos
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Id
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Training_Videos> GetTraining_VideosById(int id)
        {
            return await FarmDbContext.Training_Videos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Training_Videos> CreateTraining_Videos(Training_Videos training_Videos)
        {
            training_Videos.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(training_Videos);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Training_Videos.First(a => a.Id == training_Videos.Id);
        }

        public async Task<Training_Videos> UpdateTraining_Videos(int Id, Training_Videos training_Videos)
        {
            var training_VideosDetails = await FarmDbContext.Training_Videos.FirstOrDefaultAsync(x => x.Id == Id);
            if (training_VideosDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = training_VideosDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(training_VideosDetails, training_Videos.GetType().GetProperty(propInfo.Name).GetValue(training_Videos));
            }
            training_VideosDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Training_Videos.First(a => a.Id == training_VideosDetails.Id);
        }

	  public async Task<IEnumerable<Training_Videos>> DeleteTraining_Videos(int id)
      {
            var list = await FarmDbContext.Training_Videos.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Training_Videos.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Training_Videos> UpdateTraining_VideosStatus(int Id)
        {
            var training_VideosDetails = await FarmDbContext.Training_Videos.FirstOrDefaultAsync(x => x.Id == Id);
            if (training_VideosDetails == null)
            {
                return null;
            }
            training_VideosDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Training_Videos.First(a => a.Id == training_VideosDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Training_Videos.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchTraining_Videos(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "")
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchTraining_Videos", new SqlConnection(ConnectionString));
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