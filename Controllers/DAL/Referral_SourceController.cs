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
    public class Referral_SourceController : Controller
    {
        private readonly IReferral_SourceRepository referral_SourceRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Referral_SourceController> _logger;
	private IUtilityHelper utilityHelper;
        public Referral_SourceController(IReferral_SourceRepository referral_SourceRepository, IMapper mapper, ILogger<Referral_SourceController> logger,
	IUtilityHelper utilityHelper)
        {
            this.referral_SourceRepository = referral_SourceRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllReferral_Source")]
        public async Task<IActionResult> GetAllReferral_Source()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var referral_sourceList = await referral_SourceRepository.GetAllReferral_Source();
                _logger.LogInformation($"database call done successfully with {referral_sourceList?.Count()}");
                return Ok(referral_sourceList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetReferral_SourceById")]
        public async Task<IActionResult> GetReferral_SourceById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var referral_sourceList = await referral_SourceRepository.GetReferral_SourceById(Id);
                _logger.LogInformation($"database call done successfully with {referral_sourceList?.Id}");
                if (referral_sourceList == null)
                {
                    return NotFound();
                }
                return Ok(referral_sourceList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddReferral_Source")]
        public async Task<IActionResult> CreateReferral_Source(Referral_Source Referral_SourceDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var referral_Source = new Referral_Source()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var referral_sourceDTO = await referral_SourceRepository.CreateReferral_Source(Referral_SourceDetails);
                _logger.LogInformation($"database call done successfully with {referral_sourceDTO?.Id}");
                return Ok(referral_sourceDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateReferral_Source([FromRoute] int Id, [FromBody] Referral_Source updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Referral_Source referral_Source = await referral_SourceRepository.UpdateReferral_Source(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {referral_Source}");
                if (referral_Source == null) 
                { 
                    return NotFound(); 
                }
                return Ok(referral_Source);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateReferral_SourceStatus/{Id:int}")]
        public async Task<IActionResult> UpdateReferral_SourceStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Referral_Source referral_Source = await referral_SourceRepository.UpdateReferral_SourceStatus(Id);
                _logger.LogInformation($"database call done successfully with {referral_Source}");
                if (referral_Source == null) 
                { 
                    return NotFound(); 
                }
                return Ok(referral_Source);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteReferral_Source(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await referral_SourceRepository.DeleteReferral_Source(Id);
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

	  [HttpGet("~/GetReferral_SourceLookup")]
        public async Task<IActionResult> GetReferral_SourceLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var referral_sourceList = await referral_SourceRepository.GetReferral_SourceLookup();
                _logger.LogInformation($"database call done successfully with {referral_sourceList?.Count()}");
                return Ok(referral_sourceList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchReferral_Source")]
        public async Task<IActionResult> SearchReferral_Source(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var referral_sourceList = referral_SourceRepository.SearchReferral_Source(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {referral_sourceList?.Count()}");
                return Ok(referral_sourceList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
