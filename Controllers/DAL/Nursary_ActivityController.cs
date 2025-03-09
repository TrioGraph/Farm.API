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
    public class Nursary_ActivityController : Controller
    {
        private readonly INursary_ActivityRepository nursary_ActivityRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Nursary_ActivityController> _logger;
	private IUtilityHelper utilityHelper;
        public Nursary_ActivityController(INursary_ActivityRepository nursary_ActivityRepository, IMapper mapper, ILogger<Nursary_ActivityController> logger,
	IUtilityHelper utilityHelper)
        {
            this.nursary_ActivityRepository = nursary_ActivityRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllNursary_Activity")]
        public async Task<IActionResult> GetAllNursary_Activity()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var nursary_activityList = await nursary_ActivityRepository.GetAllNursary_Activity();
                _logger.LogInformation($"database call done successfully with {nursary_activityList?.Count()}");
                return Ok(nursary_activityList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetNursary_ActivityById")]
        public async Task<IActionResult> GetNursary_ActivityById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var nursary_activityList = await nursary_ActivityRepository.GetNursary_ActivityById(Id);
                _logger.LogInformation($"database call done successfully with {nursary_activityList?.Id}");
                if (nursary_activityList == null)
                {
                    return NotFound();
                }
                return Ok(nursary_activityList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddNursary_Activity")]
        public async Task<IActionResult> CreateNursary_Activity(Nursary_Activity Nursary_ActivityDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var nursary_Activity = new Nursary_Activity()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var nursary_activityDTO = await nursary_ActivityRepository.CreateNursary_Activity(Nursary_ActivityDetails);
                _logger.LogInformation($"database call done successfully with {nursary_activityDTO?.Id}");
                return Ok(nursary_activityDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateNursary_Activity([FromRoute] int Id, [FromBody] Nursary_Activity updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Nursary_Activity nursary_Activity = await nursary_ActivityRepository.UpdateNursary_Activity(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {nursary_Activity}");
                if (nursary_Activity == null) 
                { 
                    return NotFound(); 
                }
                return Ok(nursary_Activity);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateNursary_ActivityStatus/{Id:int}")]
        public async Task<IActionResult> UpdateNursary_ActivityStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Nursary_Activity nursary_Activity = await nursary_ActivityRepository.UpdateNursary_ActivityStatus(Id);
                _logger.LogInformation($"database call done successfully with {nursary_Activity}");
                if (nursary_Activity == null) 
                { 
                    return NotFound(); 
                }
                return Ok(nursary_Activity);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteNursary_Activity(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await nursary_ActivityRepository.DeleteNursary_Activity(Id);
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

	  [HttpGet("~/GetNursary_ActivityLookup")]
        public async Task<IActionResult> GetNursary_ActivityLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var nursary_activityList = await nursary_ActivityRepository.GetNursary_ActivityLookup();
                _logger.LogInformation($"database call done successfully with {nursary_activityList?.Count()}");
                return Ok(nursary_activityList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchNursary_Activity")]
        public async Task<IActionResult> SearchNursary_Activity(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var nursary_activityList = nursary_ActivityRepository.SearchNursary_Activity(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {nursary_activityList?.Count()}");
                return Ok(nursary_activityList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
