using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Farm.Models.Data;
using Farm.Models;
using Farm.Models.Lookup;
using Azure.Core;
using Microsoft.OpenApi.Models;

namespace Farm.Repositories
{
    public class PlantationIdentificationRepository : IPlantationIdentificationRepository
    {
        private readonly FarmDbContext FarmDbContext;

        public PlantationIdentificationRepository(FarmDbContext FarmDbContext)
        {
            this.FarmDbContext = FarmDbContext;
        }

        public async Task<List<PlantationIdentification>> GetAllPlantationIdentification()
        {
            return await FarmDbContext.PlantationIdentification.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetPlantationIdentificationLookup()
        {
            return await FarmDbContext.PlantationIdentification
            .Select(s => new
            {
                Id = s.Id,
                Name = s.First_Name
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<PlantationIdentification> GetPlantationIdentificationById(int id)
        {
            return await FarmDbContext.PlantationIdentification.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PlantationIdentification> CreatePlantationIdentification(PlantationIdentification plantationIdentification)
        {
            plantationIdentification.Id = GetNextId();
            await FarmDbContext.AddRangeAsync(plantationIdentification);
            await FarmDbContext.SaveChangesAsync();

            return this.FarmDbContext.PlantationIdentification.First(a => a.Id == plantationIdentification.Id);
        }

        public async Task<PlantationIdentification> UpdatePlantationIdentification(int Id, PlantationIdentification plantationIdentification)
        {
            var plantationIdentificationDetails = await FarmDbContext.PlantationIdentification.FirstOrDefaultAsync(x => x.Id == Id);
            if (plantationIdentificationDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = plantationIdentificationDetails.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(plantationIdentificationDetails, plantationIdentification.GetType().GetProperty(propInfo.Name).GetValue(plantationIdentification));
            }
            plantationIdentificationDetails.Id = Id;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.PlantationIdentification.First(a => a.Id == plantationIdentificationDetails.Id);
        }

        public async Task<IEnumerable<PlantationIdentification>> DeletePlantationIdentification(int id)
        {
            var list = await FarmDbContext.PlantationIdentification.Where(s => id == s.Id).ToListAsync();
            FarmDbContext.PlantationIdentification.RemoveRange(list);
            await FarmDbContext.SaveChangesAsync();
            return list;
        }

        public async Task<PlantationIdentification> UpdatePlantationIdentificationStatus(int Id)
        {
            var plantationIdentificationDetails = await FarmDbContext.PlantationIdentification.FirstOrDefaultAsync(x => x.Id == Id);
            if (plantationIdentificationDetails == null)
            {
                return null;
            }
            plantationIdentificationDetails.IsActive = false;
            await FarmDbContext.SaveChangesAsync();
            return this.FarmDbContext.PlantationIdentification.First(a => a.Id == plantationIdentificationDetails.Id);
        }

        private int GetNextId()
        {
            int? maxId = FarmDbContext.PlantationIdentification.Max(p => p.Id);
            if (maxId == null)
            {
                maxId = 0;
            }
            return ((int)maxId + 3);
        }

        public Dictionary<string, object> SearchPlantationIdentification(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection,
        bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "")
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.FarmDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchPlantationIdentification", new SqlConnection(ConnectionString));
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@userId", SqlDbType = SqlDbType.Int, Value = userId, Direction = ParameterDirection.Input });
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