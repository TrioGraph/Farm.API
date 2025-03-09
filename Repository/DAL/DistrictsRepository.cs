using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public class DistrictsRepository : IDistrictsRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public DistrictsRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<Districts>> GetAllDistricts()
        {
            return await FarmDbContext.Districts.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetDistrictsLookup()
        {
            return await FarmDbContext.Districts
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Name
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Districts> GetDistrictsById(int id)
        {
            return await FarmDbContext.Districts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Districts> CreateDistricts(Districts districts)
        {
            districts.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(districts);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.Districts.First(a => a.Id == districts.Id);
        }

        public async Task<Districts> UpdateDistricts(int Id, Districts districts)
        {
            var districtsDetails = await FarmDbContext.Districts.FirstOrDefaultAsync(x => x.Id == Id);
            if (districtsDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = districtsDetails.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(districtsDetails, districts.GetType().GetProperty(propInfo.Name).GetValue(districts));
            }
            districtsDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Districts.First(a => a.Id == districtsDetails.Id);
        }

        public async Task<IEnumerable<Districts>> DeleteDistricts(int id)
        {
            var list = await FarmDbContext.Districts.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.Districts.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
        }

        public async Task<Districts> UpdateDistrictsStatus(int Id)
        {
            var districtsDetails = await FarmDbContext.Districts.FirstOrDefaultAsync(x => x.Id == Id);
            if (districtsDetails == null)
            {
                return null;
            }
            districtsDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.Districts.First(a => a.Id == districtsDetails.Id);
        }

        private int GetNextId()
        {
            int? maxId = FarmDbContext.Districts.Max(p => p.Id);
            if (maxId == null)
            {
                maxId = 0;
            }
            return ((int)maxId + 3);
        }

        public Dictionary<string, object> SearchDistricts(int userId, SearchFields searchFields)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchDistricts", new SqlConnection(ConnectionString));

                DataTable filtersDT = new DataTable();
                //Add Columns
                filtersDT.Columns.Add("ColumnName");
                filtersDT.Columns.Add("ColumnDataType");
                filtersDT.Columns.Add("Operator");
                filtersDT.Columns.Add("Value1");
                filtersDT.Columns.Add("Value2");
                filtersDT.Columns.Add("LogicalCondition");
                //Add rows
                for(int i = 0; i < searchFields.Filters.Length;i++)
                {
                    filtersDT.Rows.Add(searchFields.Filters[i].ColumnName, searchFields.Filters[i].ColumnDataType, searchFields.Filters[i].Operator, searchFields.Filters[i].Value1, searchFields.Filters[i].Value2, searchFields.Filters[i].LogicalCondition);
                }

                string searchText = searchFields.SearchPatterns.SearchText;
                int pageNumber = (int)searchFields.SearchPatterns.PageIndex;
                int pageSize = (int)searchFields.SearchPatterns.PageSize;
                string sortColumn = searchFields.SearchPatterns.SortColumn;
                string sortOrder = searchFields.SearchPatterns.SortDirection;
                 DataTable SearchPatternsDT = new DataTable();
                //Add Columns
                SearchPatternsDT.Columns.Add("SearchText");
                SearchPatternsDT.Columns.Add("PageNumber");
                SearchPatternsDT.Columns.Add("PageSize");
                SearchPatternsDT.Columns.Add("SortColumn");
                SearchPatternsDT.Columns.Add("SortOrder");
                searchText = searchText.Replace("\"", string.Empty);
                //Add rows
                SearchPatternsDT.Rows.Add(searchText ?? "", pageNumber, pageSize, sortColumn ?? "m.Id", sortOrder ?? "DESC");

                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@userId", SqlDbType = SqlDbType.VarChar, Value = userId, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@Filters", SqlDbType = SqlDbType.Structured, Value = filtersDT, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@SearchPatterns", SqlDbType = SqlDbType.Structured, Value = SearchPatternsDT, Direction = ParameterDirection.Input  });
                
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