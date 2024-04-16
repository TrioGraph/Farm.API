using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Farm.Repositories;
using Farm.Models;
using Farm.Models.Data;
using Farm.Models.Lookup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Farm.Controllers;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Farm.Controllers
{
    [RequestFormLimits(ValueCountLimit = int.MaxValue)]
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class UtilController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly FarmDbContext FarmDbContext;

        public UtilController(IConfiguration configuration, FarmDbContext FarmDbContext)
        {
            this._configuration = configuration;
            this.FarmDbContext = FarmDbContext;
        }



        [HttpPatch]
        [Route("insertInfo")]
        public string InsertTableByColumns(dynamic info)
        {
            int nextId = 0;
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(info.ToString());
            Dictionary<string, string> insertColumnsList = jsonObj["insertColumnsList"].ToObject<Dictionary<string, string>>();
            string tableName = jsonObj["tableName"];
            int counter = 0;
            if (insertColumnsList != null && tableName != null)
            {
                SqlConnection con = new SqlConnection(this.FarmDbContext.Database.GetDbConnection().ConnectionString);
                con.Open();
                var selectMaxQuery = "SELECT Max(Id) + 1 from [lboils].[" + tableName + "]";
                SqlCommand command = new SqlCommand(selectMaxQuery, con);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    nextId = int.Parse(result.ToString());
                }


                var sql = "INSERT INTO [lboils].[" + tableName + "] (Id,";
                var valuesStr = " VALUES (" + nextId + ",";

                foreach (var item in insertColumnsList)
                {
                    if (counter == 0)
                    {
                        sql += "[" + item.Key + "]";
                        valuesStr += "@" + item.Key;
                    }
                    else
                    {
                        sql += ", [" + item.Key + "]";
                        valuesStr += ", @" + item.Key;
                    }
                    counter++;
                }
                sql += ") " + valuesStr + ")";
                counter = 0;
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                foreach (var item in insertColumnsList)
                {
                    command.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                command.Connection = con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (command.ExecuteNonQuery() > 0)
                {
                    con.Close();
                    return "" + nextId;
                }
                else
                {
                    con.Close();
                    return "" + -1;
                }
            }
            return "All Paramerers should be provided and Parameters should not be null";
        }

        [HttpPatch]
        [Route("{updateInfo}")]
        public string UpdateTableByColumns(dynamic info)
        {
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(info.ToString());
            Dictionary<string, string> updateColumnsList = jsonObj["updateColumnsList"].ToObject<Dictionary<string, string>>();
            string tableName = jsonObj["tableName"];
            Dictionary<string, string> primaryKeysList = jsonObj["primaryKeysList"].ToObject<Dictionary<string, string>>();

            var sql = "UPDATE [lboils].[" + tableName + "] SET ";
            int counter = 0;

            if (updateColumnsList != null && tableName != null && primaryKeysList != null)
            {
                foreach (var item in updateColumnsList)
                {
                    if (counter == 0)
                    {
                        sql += "[" + item.Key + "] = @" + item.Key;
                    }
                    else
                    {
                        sql += ", [" + item.Key + "] = @" + item.Key;
                    }
                    counter++;
                }

                // for (int i = 0; i < updateColumnsList.Count; i++)
                // {
                //     if (i == 0)
                //     {
                //         sql += "[" + updateColumnsList[i] + "] = @" + updateColumnsList[i];
                //     }
                //     else
                //     {
                //         sql += ", [" + updateColumnsList[i] + "] = @" + updateColumnsList[i];
                //     }
                // }

                sql += " where ";
                counter = 0;
                foreach (var item in primaryKeysList)
                {
                    if (counter == 0)
                    {
                        sql += "[" + item.Key + "] = @" + item.Key;
                    }
                    else
                    {
                        sql += " AND [" + item.Key + "] = @" + item.Key;
                    }
                    counter++;
                }

                SqlConnection con = new SqlConnection(this.FarmDbContext.Database.GetDbConnection().ConnectionString);
                SqlCommand command = new SqlCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                foreach (var item in updateColumnsList)
                {
                    command.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                foreach (var item in primaryKeysList)
                {
                    command.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                command.Connection = con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (command.ExecuteNonQuery() > 0)
                {
                    con.Close();
                    return "" + 0;
                }
                else
                {
                    con.Close();
                    return "" + -1;
                }
            }
            return "All Paramerers should be provided and Parameters should not be null";
        }

        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadImage")]
        public async Task<object> UploadImage()
        {
            try
            {
                IFormCollection formCollection;
                formCollection = Request.Form;
                var file = formCollection.Files.FirstOrDefault();

                var ftpFilePath = _configuration["ConnectionStrings:FilePath"];
                var ftpUserName = _configuration["ConnectionStrings:FileUserName"];
                var ftpPassword = _configuration["ConnectionStrings:FilePassword"];

                Uri uri = new Uri(ftpFilePath);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                // request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                // // Copy the contents of the file to the request stream.
                byte[] fileContents;
                using (StreamReader sourceStream = new StreamReader(file.OpenReadStream()))
                {
                    fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                }

                // request.ContentLength = fileContents.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    // requestStream.Write(file, 0, file.Length);
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status { response.StatusDescription}");
                }




                // using (WebClient client = new WebClient())
                // {
                //     client.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                //     using (var ftpStream = client.OpenWrite(ftpFilePath))
                //     {
                //         file.CopyTo(ftpStream);
                //     }
                // }
                return Content("Upload Successful");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            #region Old Code
            /*
             try
             {

                 // byte[] fileBytes;
                 //     using (var ms = new MemoryStream())
                 //     {
                 //         formFile.CopyTo(ms);
                 //         fileBytes = ms.ToArray();
                 //         // File processing

                 //     }
                 IFormCollection formCollection;
                 formCollection = Request.Form; // sync
                                                // var formCollection = await Request.ReadFormAsync();
                 var file = formCollection.Files.FirstOrDefault();
                 // var folderName = Path.Combine("Resources", "Images");
                 var ftpFilePath = _configuration["ConnectionStrings:FilePath"];
                 var ftpUserName = _configuration["ConnectionStrings:FileUserName"];
                 var ftpPassword = _configuration["ConnectionStrings:FilePassword"];
                 // var folderName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FilePath")["FilePath"];
                 // var pathToSave = ftpURL;
                 if (file.Length > 0)
                 {
                     // var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                     // var fullPath = Path.Combine(pathToSave, fileName);
                     // var dbPath = Path.Combine(folderName, fileName);


                     // using (var stream = new FileStream(fullPath, FileMode.Create))
                     // {
                     //     file.CopyTo(stream);
                     // }

                     FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpFilePath);
                     request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                     request.Method = WebRequestMethods.Ftp.UploadFile;

                     // using (var stream = new FileStream(fullPath, FileMode.Create))
                     // {
                     //     file.CopyTo(stream);
                     // }

                     using (Stream fileStream = System.IO.File.OpenRead(file));
                     using (Stream ftpStream = request.GetRequestStream())
                     {
                         byte[] buffer = new byte[10240];
                         int read;
                         while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                         {
                             ftpStream.Write(buffer, 0, read);
                             Console.WriteLine("Uploaded {0} bytes", fileStream.Position);
                         }
                     }

                     return Content(dbPath);
                 }
                 return Content("Error occureed during uploading image");
             }
             catch (Exception ex)
             {
                 return Content(ex.Message);
             }
             */
            #endregion
        }

        [HttpGet]
        [Route("GetImageByName")]
        public FileContentResult GetImageByName(string fileName)
        {
            byte[] b = System.IO.File.ReadAllBytes("Images//" + fileName);
            return File(b, "image/jpeg");
        }
    }
}