using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Nursary_BatchesRepository : INursary_BatchesRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Nursary_BatchesRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Nursary_Batches>> GetAllNursary_Batches()
        {
            return await FarmDbContext.Nursary_Batches.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetNursary_BatchesLookup()
        {
            return await FarmDbContext.Nursary_Batches
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Name
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Nursary_Batches> GetNursary_BatchesById(int id)
        {
            return await FarmDbContext.Nursary_Batches.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Nursary_Batches> CreateNursary_Batches(Nursary_Batches nursary_Batches)
        {
            nursary_Batches.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(nursary_Batches);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Nursary_Batches.First(a => a.Id == nursary_Batches.Id);
        }

        public async Task<Nursary_Batches> UpdateNursary_Batches(int Id, Nursary_Batches nursary_Batches)
        {
            var nursary_BatchesDetails = await FarmDbContext.Nursary_Batches.FirstOrDefaultAsync(x => x.Id == Id);
            if (nursary_BatchesDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = nursary_BatchesDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(nursary_BatchesDetails, nursary_Batches.GetType().GetProperty(propInfo.Name).GetValue(nursary_Batches));
            }
            nursary_BatchesDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Nursary_Batches.First(a => a.Id == nursary_BatchesDetails.Id);
        }

	  public async Task<IEnumerable<Nursary_Batches>> DeleteNursary_Batches(int id)
      {
            var list = await FarmDbContext.Nursary_Batches.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Nursary_Batches.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Nursary_Batches> UpdateNursary_BatchesStatus(int Id)
        {
            var nursary_BatchesDetails = await FarmDbContext.Nursary_Batches.FirstOrDefaultAsync(x => x.Id == Id);
            if (nursary_BatchesDetails == null)
            {
                return null;
            }
            nursary_BatchesDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Nursary_Batches.First(a => a.Id == nursary_BatchesDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Nursary_Batches.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchNursary_Batches(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchNursary_Batches", new SqlConnection(ConnectionString));
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