using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Poaching_FFBRepository : IPoaching_FFBRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Poaching_FFBRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Poaching_FFB>> GetAllPoaching_FFB()
        {
            return await FarmDbContext.Poaching_FFB.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetPoaching_FFBLookup()
        {
            return await FarmDbContext.Poaching_FFB
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Farmer_Id
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Poaching_FFB> GetPoaching_FFBById(int id)
        {
            return await FarmDbContext.Poaching_FFB.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Poaching_FFB> CreatePoaching_FFB(Poaching_FFB poaching_FFB)
        {
            poaching_FFB.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(poaching_FFB);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Poaching_FFB.First(a => a.Id == poaching_FFB.Id);
        }

        public async Task<Poaching_FFB> UpdatePoaching_FFB(int Id, Poaching_FFB poaching_FFB)
        {
            var poaching_FFBDetails = await FarmDbContext.Poaching_FFB.FirstOrDefaultAsync(x => x.Id == Id);
            if (poaching_FFBDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = poaching_FFBDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(poaching_FFBDetails, poaching_FFB.GetType().GetProperty(propInfo.Name).GetValue(poaching_FFB));
            }
            poaching_FFBDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Poaching_FFB.First(a => a.Id == poaching_FFBDetails.Id);
        }

	  public async Task<IEnumerable<Poaching_FFB>> DeletePoaching_FFB(int id)
      {
            var list = await FarmDbContext.Poaching_FFB.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Poaching_FFB.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Poaching_FFB> UpdatePoaching_FFBStatus(int Id)
        {
            var poaching_FFBDetails = await FarmDbContext.Poaching_FFB.FirstOrDefaultAsync(x => x.Id == Id);
            if (poaching_FFBDetails == null)
            {
                return null;
            }
            poaching_FFBDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Poaching_FFB.First(a => a.Id == poaching_FFBDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Poaching_FFB.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchPoaching_FFB(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "")
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchPoaching_FFB", new SqlConnection(ConnectionString));
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