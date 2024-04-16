using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class FarmFieldRepository : IFarmFieldRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public FarmFieldRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<FarmField>> GetAllFarmField()
        {
            return await FarmDbContext.FarmField.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetFarmFieldLookup()
        {
            return await FarmDbContext.FarmField
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Tally_Field_Id
            }).OrderByDescending(d => d.Id).ToListAsync();
        }
        public async Task<IEnumerable<object>> GetFarmFieldLookupByFarmerId(int farmerId)
        {
            return await FarmDbContext.FarmField
            .Where(d => d.Farmer_Id == farmerId)
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Tally_Field_Id
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<FarmField> GetFarmFieldById(int id)
        {
            return await FarmDbContext.FarmField.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<FarmField> CreateFarmField(FarmField farmField)
        {
            farmField.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(farmField);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.FarmField.First(a => a.Id == farmField.Id);
        }

        public async Task<FarmField> UpdateFarmField(int Id, FarmField farmField)
        {
            var farmFieldDetails = await FarmDbContext.FarmField.FirstOrDefaultAsync(x => x.Id == Id);
            if (farmFieldDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = farmFieldDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(farmFieldDetails, farmField.GetType().GetProperty(propInfo.Name).GetValue(farmField));
            }
            farmFieldDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.FarmField.First(a => a.Id == farmFieldDetails.Id);
        }

	  public async Task<IEnumerable<FarmField>> DeleteFarmField(int id)
      {
            var list = await FarmDbContext.FarmField.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.FarmField.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<FarmField> UpdateFarmFieldStatus(int Id)
        {
            var farmFieldDetails = await FarmDbContext.FarmField.FirstOrDefaultAsync(x => x.Id == Id);
            if (farmFieldDetails == null)
            {
                return null;
            }
            farmFieldDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.FarmField.First(a => a.Id == farmFieldDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.FarmField.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchFarmField(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchFarmField", new SqlConnection(ConnectionString));
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