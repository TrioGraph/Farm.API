using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;
using System.Data;
using System.Data.SqlClient;

namespace Farm.Repositories
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public AuthenticateRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }
        public async Task<Users> ValidateCredentials(string userName, string password)
        {
            return await FarmDbContext.Users.FirstOrDefaultAsync(x => x.User_Name.Equals(userName) && x.Password.Equals(password));
        }

        public async Task<string[]> GetRole_PrivilegesByRole(int? roleId)
        {
            string[] result = string.Join(",", FarmDbContext.Role_Privileges
           .Where(a => a.Role_id == roleId)
           .Select(p => p.Privilege_Id.ToString())).Split(',');

            return result;

        }

        public async Task<object> GetLookupRecentUpdates()
        {
            SqlConnection con = new SqlConnection(this.FarmDbContext.Database.GetDbConnection().ConnectionString);
            try
            {
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("LookupTablesRecentUpdates", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataset);
                if (dataset.Tables.Count > 0)
                {
                    var results = dataset.Tables[0].AsEnumerable().
                                      Select(row => new
                                      {
                                          LookupTableName = row.Field<int>("LookupTableName"),
                                          MaxDate = row.Field<string>("MaxDate"),
                                      });
                    return results;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex, ex.ToString());
                throw;
            }
            finally
            {
                con.Close();
            }
        }

    }
}