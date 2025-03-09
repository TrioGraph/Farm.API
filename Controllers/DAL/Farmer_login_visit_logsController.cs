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
    public class Farmer_login_visit_logsController : Controller
    {
        private readonly IFarmer_login_visit_logsRepository farmer_login_visit_logsRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Farmer_login_visit_logsController> _logger;
	private IUtilityHelper utilityHelper;
        public Farmer_login_visit_logsController(IFarmer_login_visit_logsRepository farmer_login_visit_logsRepository, IMapper mapper, ILogger<Farmer_login_visit_logsController> logger,
	IUtilityHelper utilityHelper)
        {
            this.farmer_login_visit_logsRepository = farmer_login_visit_logsRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllFarmer_login_visit_logs")]
        public async Task<IActionResult> GetAllFarmer_login_visit_logs()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var farmer_login_visit_logsList = await farmer_login_visit_logsRepository.GetAllFarmer_login_visit_logs();
                _logger.LogInformation($"database call done successfully with {farmer_login_visit_logsList?.Count()}");
                return Ok(farmer_login_visit_logsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetFarmer_login_visit_logsById")]
        public async Task<IActionResult> GetFarmer_login_visit_logsById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var farmer_login_visit_logsList = await farmer_login_visit_logsRepository.GetFarmer_login_visit_logsById(Id);
                _logger.LogInformation($"database call done successfully with {farmer_login_visit_logsList?.Id}");
                if (farmer_login_visit_logsList == null)
                {
                    return NotFound();
                }
                return Ok(farmer_login_visit_logsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddFarmer_login_visit_logs")]
        public async Task<IActionResult> CreateFarmer_login_visit_logs(Farmer_login_visit_logs Farmer_login_visit_logsDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var farmer_login_visit_logs = new Farmer_login_visit_logs()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var farmer_login_visit_logsDTO = await farmer_login_visit_logsRepository.CreateFarmer_login_visit_logs(Farmer_login_visit_logsDetails);
                _logger.LogInformation($"database call done successfully with {farmer_login_visit_logsDTO?.Id}");
                return Ok(farmer_login_visit_logsDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateFarmer_login_visit_logs([FromRoute] int Id, [FromBody] Farmer_login_visit_logs updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farmer_login_visit_logs farmer_login_visit_logs = await farmer_login_visit_logsRepository.UpdateFarmer_login_visit_logs(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {farmer_login_visit_logs}");
                if (farmer_login_visit_logs == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmer_login_visit_logs);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateFarmer_login_visit_logsStatus/{Id:int}")]
        public async Task<IActionResult> UpdateFarmer_login_visit_logsStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farmer_login_visit_logs farmer_login_visit_logs = await farmer_login_visit_logsRepository.UpdateFarmer_login_visit_logsStatus(Id);
                _logger.LogInformation($"database call done successfully with {farmer_login_visit_logs}");
                if (farmer_login_visit_logs == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmer_login_visit_logs);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteFarmer_login_visit_logs(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await farmer_login_visit_logsRepository.DeleteFarmer_login_visit_logs(Id);
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

	  [HttpGet("~/GetFarmer_login_visit_logsLookup")]
        public async Task<IActionResult> GetFarmer_login_visit_logsLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var farmer_login_visit_logsList = await farmer_login_visit_logsRepository.GetFarmer_login_visit_logsLookup();
                _logger.LogInformation($"database call done successfully with {farmer_login_visit_logsList?.Count()}");
                return Ok(farmer_login_visit_logsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchFarmer_login_visit_logs")]
        public async Task<IActionResult> SearchFarmer_login_visit_logs(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var farmer_login_visit_logsList = farmer_login_visit_logsRepository.SearchFarmer_login_visit_logs(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {farmer_login_visit_logsList?.Count()}");
                return Ok(farmer_login_visit_logsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
