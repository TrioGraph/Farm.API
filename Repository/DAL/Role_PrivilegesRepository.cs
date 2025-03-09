using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Role_PrivilegesRepository : IRole_PrivilegesRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Role_PrivilegesRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Role_Privileges>> GetAllRole_Privileges()
        {
            return await FarmDbContext.Role_Privileges.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetRole_PrivilegesLookup()
        {
            return await FarmDbContext.Role_Privileges
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Privilege_Id
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Role_Privileges> GetRole_PrivilegesById(int id)
        {
            return await FarmDbContext.Role_Privileges.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Role_Privileges> CreateRole_Privileges(Role_Privileges role_Privileges)
        {
            role_Privileges.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(role_Privileges);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Role_Privileges.First(a => a.Id == role_Privileges.Id);
        }

        public async Task<Role_Privileges> UpdateRole_Privileges(int Id, Role_Privileges role_Privileges)
        {
            var role_PrivilegesDetails = await FarmDbContext.Role_Privileges.FirstOrDefaultAsync(x => x.Id == Id);
            if (role_PrivilegesDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = role_PrivilegesDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(role_PrivilegesDetails, role_Privileges.GetType().GetProperty(propInfo.Name).GetValue(role_Privileges));
            }
            role_PrivilegesDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Role_Privileges.First(a => a.Id == role_PrivilegesDetails.Id);
        }

	  public async Task<IEnumerable<Role_Privileges>> DeleteRole_Privileges(int id)
      {
            var list = await FarmDbContext.Role_Privileges.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Role_Privileges.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Role_Privileges> UpdateRole_PrivilegesStatus(int Id)
        {
            var role_PrivilegesDetails = await FarmDbContext.Role_Privileges.FirstOrDefaultAsync(x => x.Id == Id);
            if (role_PrivilegesDetails == null)
            {
                return null;
            }
            role_PrivilegesDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Role_Privileges.First(a => a.Id == role_PrivilegesDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Role_Privileges.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchRole_Privileges(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "")
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchRole_Privileges", new SqlConnection(ConnectionString));
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