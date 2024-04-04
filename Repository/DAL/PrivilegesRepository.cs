using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class PrivilegesRepository : IPrivilegesRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public PrivilegesRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Privileges>> GetAllPrivileges()
        {
            return await FarmDbContext.Privileges.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetPrivilegesLookup()
        {
            return await FarmDbContext.Privileges
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Privilege
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Privileges> GetPrivilegesById(int id)
        {
            return await FarmDbContext.Privileges.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Privileges> CreatePrivileges(Privileges privileges)
        {
            privileges.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(privileges);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Privileges.First(a => a.Id == privileges.Id);
        }

        public async Task<Privileges> UpdatePrivileges(int Id, Privileges privileges)
        {
            var privilegesDetails = await FarmDbContext.Privileges.FirstOrDefaultAsync(x => x.Id == Id);
            if (privilegesDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = privilegesDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(privilegesDetails, privileges.GetType().GetProperty(propInfo.Name).GetValue(privileges));
            }
            privilegesDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Privileges.First(a => a.Id == privilegesDetails.Id);
        }

	  public async Task<IEnumerable<Privileges>> DeletePrivileges(int id)
      {
            var list = await FarmDbContext.Privileges.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Privileges.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Privileges> UpdatePrivilegesStatus(int Id)
        {
            var privilegesDetails = await FarmDbContext.Privileges.FirstOrDefaultAsync(x => x.Id == Id);
            if (privilegesDetails == null)
            {
                return null;
            }
            privilegesDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Privileges.First(a => a.Id == privilegesDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Privileges.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchPrivileges(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchPrivileges", new SqlConnection(ConnectionString));
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