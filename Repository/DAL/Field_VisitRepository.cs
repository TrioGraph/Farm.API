using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class Field_VisitRepository : IField_VisitRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public Field_VisitRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Field_Visit>> GetAllField_Visit()
        {
            return await FarmDbContext.Field_Visit.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetField_VisitLookup()
        {
            return await FarmDbContext.Field_Visit
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Field_Id
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Field_Visit> GetField_VisitById(int id)
        {
            return await FarmDbContext.Field_Visit.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Field_Visit> CreateField_Visit(Field_Visit field_Visit)
        {
            field_Visit.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(field_Visit);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Field_Visit.First(a => a.Id == field_Visit.Id);
        }

        public async Task<Field_Visit> UpdateField_Visit(int Id, Field_Visit field_Visit)
        {
            var field_VisitDetails = await FarmDbContext.Field_Visit.FirstOrDefaultAsync(x => x.Id == Id);
            if (field_VisitDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = field_VisitDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(field_VisitDetails, field_Visit.GetType().GetProperty(propInfo.Name).GetValue(field_Visit));
            }
            field_VisitDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Field_Visit.First(a => a.Id == field_VisitDetails.Id);
        }

	  public async Task<IEnumerable<Field_Visit>> DeleteField_Visit(int id)
      {
            var list = await FarmDbContext.Field_Visit.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Field_Visit.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Field_Visit> UpdateField_VisitStatus(int Id)
        {
            var field_VisitDetails = await FarmDbContext.Field_Visit.FirstOrDefaultAsync(x => x.Id == Id);
            if (field_VisitDetails == null)
            {
                return null;
            }
            field_VisitDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Field_Visit.First(a => a.Id == field_VisitDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = FarmDbContext.Field_Visit.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchField_Visit(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchField_Visit", new SqlConnection(ConnectionString));
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