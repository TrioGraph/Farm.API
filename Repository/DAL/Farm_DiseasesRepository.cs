using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Farm_DiseasesRepository : IFarm_DiseasesRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Farm_DiseasesRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Farm_Diseases>> GetAllFarm_Diseases()
        {
            return await FarmDbContext.Farm_Diseases.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetFarm_DiseasesLookup()
        {
            return await FarmDbContext.Farm_Diseases
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Name
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Farm_Diseases> GetFarm_DiseasesById(int id)
        {
            return await FarmDbContext.Farm_Diseases.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Farm_Diseases> CreateFarm_Diseases(Farm_Diseases farm_Diseases)
        {
            farm_Diseases.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(farm_Diseases);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Farm_Diseases.First(a => a.Id == farm_Diseases.Id);
        }

        public async Task<Farm_Diseases> UpdateFarm_Diseases(int Id, Farm_Diseases farm_Diseases)
        {
            var farm_DiseasesDetails = await FarmDbContext.Farm_Diseases.FirstOrDefaultAsync(x => x.Id == Id);
            if (farm_DiseasesDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = farm_DiseasesDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(farm_DiseasesDetails, farm_Diseases.GetType().GetProperty(propInfo.Name).GetValue(farm_Diseases));
            }
            farm_DiseasesDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Farm_Diseases.First(a => a.Id == farm_DiseasesDetails.Id);
        }

	  public async Task<IEnumerable<Farm_Diseases>> DeleteFarm_Diseases(int id)
      {
            var list = await FarmDbContext.Farm_Diseases.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Farm_Diseases.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Farm_Diseases> UpdateFarm_DiseasesStatus(int Id)
        {
            var farm_DiseasesDetails = await FarmDbContext.Farm_Diseases.FirstOrDefaultAsync(x => x.Id == Id);
            if (farm_DiseasesDetails == null)
            {
                return null;
            }
            farm_DiseasesDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Farm_Diseases.First(a => a.Id == farm_DiseasesDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Farm_Diseases.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchFarm_Diseases(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchFarm_Diseases", new SqlConnection(ConnectionString));
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