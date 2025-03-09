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
    public class Farmer_GroupController : Controller
    {
        private readonly IFarmer_GroupRepository farmer_GroupRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Farmer_GroupController> _logger;
	private IUtilityHelper utilityHelper;
        public Farmer_GroupController(IFarmer_GroupRepository farmer_GroupRepository, IMapper mapper, ILogger<Farmer_GroupController> logger,
	IUtilityHelper utilityHelper)
        {
            this.farmer_GroupRepository = farmer_GroupRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllFarmer_Group")]
        public async Task<IActionResult> GetAllFarmer_Group()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var farmer_groupList = await farmer_GroupRepository.GetAllFarmer_Group();
                _logger.LogInformation($"database call done successfully with {farmer_groupList?.Count()}");
                return Ok(farmer_groupList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetFarmer_GroupById")]
        public async Task<IActionResult> GetFarmer_GroupById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var farmer_groupList = await farmer_GroupRepository.GetFarmer_GroupById(Id);
                _logger.LogInformation($"database call done successfully with {farmer_groupList?.Id}");
                if (farmer_groupList == null)
                {
                    return NotFound();
                }
                return Ok(farmer_groupList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddFarmer_Group")]
        public async Task<IActionResult> CreateFarmer_Group(Farmer_Group Farmer_GroupDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var farmer_Group = new Farmer_Group()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var farmer_groupDTO = await farmer_GroupRepository.CreateFarmer_Group(Farmer_GroupDetails);
                _logger.LogInformation($"database call done successfully with {farmer_groupDTO?.Id}");
                return Ok(farmer_groupDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateFarmer_Group([FromRoute] int Id, [FromBody] Farmer_Group updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farmer_Group farmer_Group = await farmer_GroupRepository.UpdateFarmer_Group(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {farmer_Group}");
                if (farmer_Group == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmer_Group);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateFarmer_GroupStatus/{Id:int}")]
        public async Task<IActionResult> UpdateFarmer_GroupStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farmer_Group farmer_Group = await farmer_GroupRepository.UpdateFarmer_GroupStatus(Id);
                _logger.LogInformation($"database call done successfully with {farmer_Group}");
                if (farmer_Group == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmer_Group);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteFarmer_Group(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await farmer_GroupRepository.DeleteFarmer_Group(Id);
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

	  [HttpGet("~/GetFarmer_GroupLookup")]
        public async Task<IActionResult> GetFarmer_GroupLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var farmer_groupList = await farmer_GroupRepository.GetFarmer_GroupLookup();
                _logger.LogInformation($"database call done successfully with {farmer_groupList?.Count()}");
                return Ok(farmer_groupList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchFarmer_Group")]
        public async Task<IActionResult> SearchFarmer_Group(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var farmer_groupList = farmer_GroupRepository.SearchFarmer_Group(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {farmer_groupList?.Count()}");
                return Ok(farmer_groupList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
