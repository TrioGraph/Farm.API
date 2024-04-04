using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Mandal_BlocksRepository : IMandal_BlocksRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Mandal_BlocksRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Mandal_Blocks>> GetAllMandal_Blocks()
        {
            return await FarmDbContext.Mandal_Blocks.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetMandal_BlocksLookup()
        {
            return await FarmDbContext.Mandal_Blocks
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Name
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Mandal_Blocks> GetMandal_BlocksById(int id)
        {
            return await FarmDbContext.Mandal_Blocks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Mandal_Blocks> CreateMandal_Blocks(Mandal_Blocks mandal_Blocks)
        {
            mandal_Blocks.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(mandal_Blocks);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Mandal_Blocks.First(a => a.Id == mandal_Blocks.Id);
        }

        public async Task<Mandal_Blocks> UpdateMandal_Blocks(int Id, Mandal_Blocks mandal_Blocks)
        {
            var mandal_BlocksDetails = await FarmDbContext.Mandal_Blocks.FirstOrDefaultAsync(x => x.Id == Id);
            if (mandal_BlocksDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = mandal_BlocksDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(mandal_BlocksDetails, mandal_Blocks.GetType().GetProperty(propInfo.Name).GetValue(mandal_Blocks));
            }
            mandal_BlocksDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Mandal_Blocks.First(a => a.Id == mandal_BlocksDetails.Id);
        }

	  public async Task<IEnumerable<Mandal_Blocks>> DeleteMandal_Blocks(int id)
      {
            var list = await FarmDbContext.Mandal_Blocks.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Mandal_Blocks.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Mandal_Blocks> UpdateMandal_BlocksStatus(int Id)
        {
            var mandal_BlocksDetails = await FarmDbContext.Mandal_Blocks.FirstOrDefaultAsync(x => x.Id == Id);
            if (mandal_BlocksDetails == null)
            {
                return null;
            }
            mandal_BlocksDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Mandal_Blocks.First(a => a.Id == mandal_BlocksDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Mandal_Blocks.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchMandal_Blocks(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchMandal_Blocks", new SqlConnection(ConnectionString));
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