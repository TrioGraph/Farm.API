using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Employee_RolesRepository : IEmployee_RolesRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Employee_RolesRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Employee_Roles>> GetAllEmployee_Roles()
        {
            return await FarmDbContext.Employee_Roles.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetEmployee_RolesLookup()
        {
            return await FarmDbContext.Employee_Roles
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Roles_Id
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Employee_Roles> GetEmployee_RolesById(int id)
        {
            return await FarmDbContext.Employee_Roles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee_Roles> CreateEmployee_Roles(Employee_Roles employee_Roles)
        {
            employee_Roles.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(employee_Roles);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Employee_Roles.First(a => a.Id == employee_Roles.Id);
        }

        public async Task<Employee_Roles> UpdateEmployee_Roles(int Id, Employee_Roles employee_Roles)
        {
            var employee_RolesDetails = await FarmDbContext.Employee_Roles.FirstOrDefaultAsync(x => x.Id == Id);
            if (employee_RolesDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = employee_RolesDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(employee_RolesDetails, employee_Roles.GetType().GetProperty(propInfo.Name).GetValue(employee_Roles));
            }
            employee_RolesDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Employee_Roles.First(a => a.Id == employee_RolesDetails.Id);
        }

	  public async Task<IEnumerable<Employee_Roles>> DeleteEmployee_Roles(int id)
      {
            var list = await FarmDbContext.Employee_Roles.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Employee_Roles.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Employee_Roles> UpdateEmployee_RolesStatus(int Id)
        {
            var employee_RolesDetails = await FarmDbContext.Employee_Roles.FirstOrDefaultAsync(x => x.Id == Id);
            if (employee_RolesDetails == null)
            {
                return null;
            }
            employee_RolesDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Employee_Roles.First(a => a.Id == employee_RolesDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Employee_Roles.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchEmployee_Roles(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "")
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchEmployee_Roles", new SqlConnection(ConnectionString));
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