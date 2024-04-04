using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public RolesRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Roles>> GetAllRoles()
        {
            return await FarmDbContext.Roles.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetRolesLookup()
        {
            return await FarmDbContext.Roles
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Role
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Roles> GetRolesById(int id)
        {
            return await FarmDbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Roles> CreateRoles(Roles roles)
        {
            roles.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(roles);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Roles.First(a => a.Id == roles.Id);
        }

        public async Task<Roles> UpdateRoles(int Id, Roles roles)
        {
            var rolesDetails = await FarmDbContext.Roles.FirstOrDefaultAsync(x => x.Id == Id);
            if (rolesDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = rolesDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(rolesDetails, roles.GetType().GetProperty(propInfo.Name).GetValue(roles));
            }
            rolesDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Roles.First(a => a.Id == rolesDetails.Id);
        }

	  public async Task<IEnumerable<Roles>> DeleteRoles(int id)
      {
            var list = await FarmDbContext.Roles.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Roles.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Roles> UpdateRolesStatus(int Id)
        {
            var rolesDetails = await FarmDbContext.Roles.FirstOrDefaultAsync(x => x.Id == Id);
            if (rolesDetails == null)
            {
                return null;
            }
            rolesDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Roles.First(a => a.Id == rolesDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Roles.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchRoles(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchRoles", new SqlConnection(ConnectionString));
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