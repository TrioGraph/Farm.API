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
    public class CampaignsController : Controller
    {
        private readonly ICampaignsRepository campaignsRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CampaignsController> _logger;
	private IUtilityHelper utilityHelper;
        public CampaignsController(ICampaignsRepository campaignsRepository, IMapper mapper, ILogger<CampaignsController> logger,
	IUtilityHelper utilityHelper)
        {
            this.campaignsRepository = campaignsRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllCampaigns")]
        public async Task<IActionResult> GetAllCampaigns()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var campaignsList = await campaignsRepository.GetAllCampaigns();
                _logger.LogInformation($"database call done successfully with {campaignsList?.Count()}");
                return Ok(campaignsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetCampaignsById")]
        public async Task<IActionResult> GetCampaignsById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var campaignsList = await campaignsRepository.GetCampaignsById(Id);
                _logger.LogInformation($"database call done successfully with {campaignsList?.Id}");
                if (campaignsList == null)
                {
                    return NotFound();
                }
                return Ok(campaignsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddCampaigns")]
        public async Task<IActionResult> CreateCampaigns(Campaigns CampaignsDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var campaigns = new Campaigns()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var campaignsDTO = await campaignsRepository.CreateCampaigns(CampaignsDetails);
                _logger.LogInformation($"database call done successfully with {campaignsDTO?.Id}");
                return Ok(campaignsDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateCampaigns([FromRoute] int Id, [FromBody] Campaigns updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Campaigns campaigns = await campaignsRepository.UpdateCampaigns(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {campaigns}");
                if (campaigns == null) 
                { 
                    return NotFound(); 
                }
                return Ok(campaigns);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateCampaignsStatus/{Id:int}")]
        public async Task<IActionResult> UpdateCampaignsStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Campaigns campaigns = await campaignsRepository.UpdateCampaignsStatus(Id);
                _logger.LogInformation($"database call done successfully with {campaigns}");
                if (campaigns == null) 
                { 
                    return NotFound(); 
                }
                return Ok(campaigns);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteCampaigns(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await campaignsRepository.DeleteCampaigns(Id);
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

	  [HttpGet("~/GetCampaignsLookup")]
        public async Task<IActionResult> GetCampaignsLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var campaignsList = await campaignsRepository.GetCampaignsLookup();
                _logger.LogInformation($"database call done successfully with {campaignsList?.Count()}");
                return Ok(campaignsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchCampaigns")]
        public async Task<IActionResult> SearchCampaigns(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var campaignsList = campaignsRepository.SearchCampaigns(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {campaignsList?.Count()}");
                return Ok(campaignsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
