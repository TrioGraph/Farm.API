using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Farm.Repositories;
using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class Logins_LogController : Controller
    {
        private readonly ILogins_LogRepository logins_LogRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Logins_LogController> _logger;
	private IUtilityHelper utilityHelper;
        public Logins_LogController(ILogins_LogRepository logins_LogRepository, IMapper mapper, ILogger<Logins_LogController> logger,
	IUtilityHelper utilityHelper)
        {
            this.logins_LogRepository = logins_LogRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllLogins_Log")]
        public async Task<IActionResult> GetAllLogins_Log()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var logins_logList = await logins_LogRepository.GetAllLogins_Log();
                _logger.LogInformation($"database call done successfully with {logins_logList?.Count()}");
                return Ok(logins_logList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetLogins_LogById")]
        public async Task<IActionResult> GetLogins_LogById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var logins_logList = await logins_LogRepository.GetLogins_LogById(Id);
                _logger.LogInformation($"database call done successfully with {logins_logList?.Id}");
                if (logins_logList == null)
                {
                    return NotFound();
                }
                return Ok(logins_logList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddLogins_Log")]
        public async Task<IActionResult> CreateLogins_Log(Logins_Log Logins_LogDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var logins_Log = new Logins_Log()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var logins_logDTO = await logins_LogRepository.CreateLogins_Log(Logins_LogDetails);
                _logger.LogInformation($"database call done successfully with {logins_logDTO?.Id}");
                return Ok(logins_logDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateLogins_Log([FromRoute] int Id, [FromBody] Logins_Log updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Logins_Log logins_Log = await logins_LogRepository.UpdateLogins_Log(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {logins_Log}");
                if (logins_Log == null) 
                { 
                    return NotFound(); 
                }
                return Ok(logins_Log);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateLogins_LogStatus/{Id:int}")]
        public async Task<IActionResult> UpdateLogins_LogStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Logins_Log logins_Log = await logins_LogRepository.UpdateLogins_LogStatus(Id);
                _logger.LogInformation($"database call done successfully with {logins_Log}");
                if (logins_Log == null) 
                { 
                    return NotFound(); 
                }
                return Ok(logins_Log);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteLogins_Log(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await logins_LogRepository.DeleteLogins_Log(Id);
                _logger.LogInformation($"database call done successfully with {deletedItem}");
                if (deletedItem == null)
                {
                    return NotFound();
                }
                return Ok(deletedItem);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpGet("~/GetLogins_LogLookup")]
        public async Task<IActionResult> GetLogins_LogLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var logins_logList = await logins_LogRepository.GetLogins_LogLookup();
                _logger.LogInformation($"database call done successfully with {logins_logList?.Count()}");
                return Ok(logins_logList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchLogins_Log")]
        public async Task<IActionResult> SearchLogins_Log(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
        bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
		string userId = utilityHelper.GetUserFromRequest(Request);
                var logins_logList = logins_LogRepository.SearchLogins_Log(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {logins_logList?.Count()}");
                return Ok(logins_logList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
