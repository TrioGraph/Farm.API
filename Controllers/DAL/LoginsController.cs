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
    public class LoginsController : Controller
    {
        private readonly ILoginsRepository loginsRepository;
        private readonly IMapper mapper;
        private readonly ILogger<LoginsController> _logger;
        public LoginsController(ILoginsRepository loginsRepository, IMapper mapper, ILogger<LoginsController> logger)
        {
            this.loginsRepository = loginsRepository;
            mapper = mapper;
            _logger = logger;
        }

        [HttpGet("~/GetAllLogins")]
        public async Task<IActionResult> GetAllLogins()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var loginsList = await loginsRepository.GetAllLogins();
                _logger.LogInformation($"database call done successfully with {loginsList?.Count()}");
                return Ok(loginsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetLoginsById")]
        public async Task<IActionResult> GetLoginsById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var loginsList = await loginsRepository.GetLoginsById(Id);
                _logger.LogInformation($"database call done successfully with {loginsList?.Id}");
                if (loginsList == null)
                {
                    return NotFound();
                }
                return Ok(loginsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddLogins")]
        public async Task<IActionResult> CreateLogins(Logins LoginsDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var logins = new Logins()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var loginsDTO = await loginsRepository.CreateLogins(LoginsDetails);
                _logger.LogInformation($"database call done successfully with {loginsDTO?.Id}");
                return Ok(loginsDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateLogins([FromRoute] int Id, [FromBody] Logins updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Logins logins = await loginsRepository.UpdateLogins(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {logins}");
                if (logins == null) 
                { 
                    return NotFound(); 
                }
                return Ok(logins);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateLoginsStatus/{Id:int}")]
        public async Task<IActionResult> UpdateLoginsStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Logins logins = await loginsRepository.UpdateLoginsStatus(Id);
                _logger.LogInformation($"database call done successfully with {logins}");
                if (logins == null) 
                { 
                    return NotFound(); 
                }
                return Ok(logins);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteLogins(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await loginsRepository.DeleteLogins(Id);
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

	  [HttpGet("~/GetLoginsLookup")]
        public async Task<IActionResult> GetLoginsLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var loginsList = await loginsRepository.GetLoginsLookup();
                _logger.LogInformation($"database call done successfully with {loginsList?.Count()}");
                return Ok(loginsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchLogins")]
        public async Task<IActionResult> SearchLogins(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
                var loginsList = loginsRepository.SearchLogins(searchText, pageNumber, pageSize, sortColumn, sortOrder);
                _logger.LogInformation($"database call done successfully with {loginsList?.Count()}");
                return Ok(loginsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
