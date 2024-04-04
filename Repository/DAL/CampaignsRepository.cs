using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class CampaignsRepository : ICampaignsRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public CampaignsRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Campaigns>> GetAllCampaigns()
        {
            return await FarmDbContext.Campaigns.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetCampaignsLookup()
        {
            return await FarmDbContext.Campaigns
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Name
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Campaigns> GetCampaignsById(int id)
        {
            return await FarmDbContext.Campaigns.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Campaigns> CreateCampaigns(Campaigns campaigns)
        {
            campaigns.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(campaigns);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Campaigns.First(a => a.Id == campaigns.Id);
        }

        public async Task<Campaigns> UpdateCampaigns(int Id, Campaigns campaigns)
        {
            var campaignsDetails = await FarmDbContext.Campaigns.FirstOrDefaultAsync(x => x.Id == Id);
            if (campaignsDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = campaignsDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(campaignsDetails, campaigns.GetType().GetProperty(propInfo.Name).GetValue(campaigns));
            }
            campaignsDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Campaigns.First(a => a.Id == campaignsDetails.Id);
        }

	  public async Task<IEnumerable<Campaigns>> DeleteCampaigns(int id)
      {
            var list = await FarmDbContext.Campaigns.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Campaigns.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Campaigns> UpdateCampaignsStatus(int Id)
        {
            var campaignsDetails = await FarmDbContext.Campaigns.FirstOrDefaultAsync(x => x.Id == Id);
            if (campaignsDetails == null)
            {
                return null;
            }
            campaignsDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Campaigns.First(a => a.Id == campaignsDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Campaigns.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchCampaigns(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchCampaigns", new SqlConnection(ConnectionString));
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