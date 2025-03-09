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
    public class Campaign_TypesController : Controller
    {
        private readonly ICampaign_TypesRepository campaign_TypesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Campaign_TypesController> _logger;
	private IUtilityHelper utilityHelper;
        public Campaign_TypesController(ICampaign_TypesRepository campaign_TypesRepository, IMapper mapper, ILogger<Campaign_TypesController> logger,
	IUtilityHelper utilityHelper)
        {
            this.campaign_TypesRepository = campaign_TypesRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllCampaign_Types")]
        public async Task<IActionResult> GetAllCampaign_Types()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var campaign_typesList = await campaign_TypesRepository.GetAllCampaign_Types();
                _logger.LogInformation($"database call done successfully with {campaign_typesList?.Count()}");
                return Ok(campaign_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetCampaign_TypesById")]
        public async Task<IActionResult> GetCampaign_TypesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var campaign_typesList = await campaign_TypesRepository.GetCampaign_TypesById(Id);
                _logger.LogInformation($"database call done successfully with {campaign_typesList?.Id}");
                if (campaign_typesList == null)
                {
                    return NotFound();
                }
                return Ok(campaign_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddCampaign_Types")]
        public async Task<IActionResult> CreateCampaign_Types(Campaign_Types Campaign_TypesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var campaign_Types = new Campaign_Types()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var campaign_typesDTO = await campaign_TypesRepository.CreateCampaign_Types(Campaign_TypesDetails);
                _logger.LogInformation($"database call done successfully with {campaign_typesDTO?.Id}");
                return Ok(campaign_typesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateCampaign_Types([FromRoute] int Id, [FromBody] Campaign_Types updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Campaign_Types campaign_Types = await campaign_TypesRepository.UpdateCampaign_Types(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {campaign_Types}");
                if (campaign_Types == null) 
                { 
                    return NotFound(); 
                }
                return Ok(campaign_Types);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateCampaign_TypesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateCampaign_TypesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Campaign_Types campaign_Types = await campaign_TypesRepository.UpdateCampaign_TypesStatus(Id);
                _logger.LogInformation($"database call done successfully with {campaign_Types}");
                if (campaign_Types == null) 
                { 
                    return NotFound(); 
                }
                return Ok(campaign_Types);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteCampaign_Types(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await campaign_TypesRepository.DeleteCampaign_Types(Id);
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

	  [HttpGet("~/GetCampaign_TypesLookup")]
        public async Task<IActionResult> GetCampaign_TypesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var campaign_typesList = await campaign_TypesRepository.GetCampaign_TypesLookup();
                _logger.LogInformation($"database call done successfully with {campaign_typesList?.Count()}");
                return Ok(campaign_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchCampaign_Types")]
        public async Task<IActionResult> SearchCampaign_Types(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var campaign_typesList = campaign_TypesRepository.SearchCampaign_Types(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {campaign_typesList?.Count()}");
                return Ok(campaign_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
