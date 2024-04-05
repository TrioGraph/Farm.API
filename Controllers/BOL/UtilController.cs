using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Farm.Repositories;
using Farm.Models;
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

namespace Farm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class UtilController : Controller
    {
        [HttpPatch]
        [Route("{updateInfo}")]
        public string UpdateTableByColumns(Dictionary<string, object> updateInfo)
        {
            Dictionary<string, object> updateColumnsList = updateInfo["updateColumnsList"] as Dictionary<string, object>;
            string tableName = updateInfo["tableName"] as string;
            Dictionary<string, object> primaryKeysList = updateInfo["primaryKeysList"] as Dictionary<string, object>;
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
                    // foo(item.Key);
                    // bar(item.Value);
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
                foreach (KeyValuePair<string, object> item in primaryKeysList)
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

                SqlConnection con = new SqlConnection("");
                SqlCommand command = new SqlCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> item in updateColumnsList)
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
                    return "SUCCESS";
                }
                else
                {
                    con.Close();
                    return "Fail";
                }
            }
            return "All Paramerers should be provided and Parameters should not be null";
        }


        [HttpPatch]
        [Route("{insertInfo}")]
        public string InsertTableByColumns(Dictionary<string, object> insertInfo)
        {
            Dictionary<string, object> insertColumnsList = insertInfo["insertColumnsList"] as Dictionary<string, object>;
            string tableName = insertInfo["tableName"] as string;
            var sql = "INSERT INTO [lboils].[" + tableName + "] (";
            var valuesStr = "";
            int counter = 0;
            
            if (insertColumnsList != null && tableName != null)
            {
                foreach (var item in insertColumnsList)
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
                    // foo(item.Key);
                    // bar(item.Value);
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
                

                SqlConnection con = new SqlConnection("");
                SqlCommand command = new SqlCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> item in insertColumnsList)
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
                    return "SUCCESS";
                }
                else
                {
                    con.Close();
                    return "Fail";
                }
            }
            return "All Paramerers should be provided and Parameters should not be null";
        }
    }
}