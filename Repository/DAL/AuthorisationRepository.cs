using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class AuthorisationRepository : IAuthorisationRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public AuthorisationRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Authorisation>> GetAllAuthorisation()
        {
            return await FarmDbContext.Authorisation.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAuthorisationLookup()
        {
            return await FarmDbContext.Authorisation
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Name
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Authorisation> GetAuthorisationById(int id)
        {
            return await FarmDbContext.Authorisation.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Authorisation> CreateAuthorisation(Authorisation authorisation)
        {
            authorisation.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(authorisation);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Authorisation.First(a => a.Id == authorisation.Id);
        }

        public async Task<Authorisation> UpdateAuthorisation(int Id, Authorisation authorisation)
        {
            var authorisationDetails = await FarmDbContext.Authorisation.FirstOrDefaultAsync(x => x.Id == Id);
            if (authorisationDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = authorisationDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(authorisationDetails, authorisation.GetType().GetProperty(propInfo.Name).GetValue(authorisation));
            }
            authorisationDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Authorisation.First(a => a.Id == authorisationDetails.Id);
        }

	  public async Task<IEnumerable<Authorisation>> DeleteAuthorisation(int id)
      {
            var list = await FarmDbContext.Authorisation.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Authorisation.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Authorisation> UpdateAuthorisationStatus(int Id)
        {
            var authorisationDetails = await FarmDbContext.Authorisation.FirstOrDefaultAsync(x => x.Id == Id);
            if (authorisationDetails == null)
            {
                return null;
            }
            authorisationDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Authorisation.First(a => a.Id == authorisationDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Authorisation.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchAuthorisation(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchAuthorisation", new SqlConnection(ConnectionString));
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