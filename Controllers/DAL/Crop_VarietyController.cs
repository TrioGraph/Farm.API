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
    public class Crop_VarietyController : Controller
    {
        private readonly ICrop_VarietyRepository crop_VarietyRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Crop_VarietyController> _logger;
	private IUtilityHelper utilityHelper;
        public Crop_VarietyController(ICrop_VarietyRepository crop_VarietyRepository, IMapper mapper, ILogger<Crop_VarietyController> logger,
	IUtilityHelper utilityHelper)
        {
            this.crop_VarietyRepository = crop_VarietyRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllCrop_Variety")]
        public async Task<IActionResult> GetAllCrop_Variety()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var crop_varietyList = await crop_VarietyRepository.GetAllCrop_Variety();
                _logger.LogInformation($"database call done successfully with {crop_varietyList?.Count()}");
                return Ok(crop_varietyList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetCrop_VarietyById")]
        public async Task<IActionResult> GetCrop_VarietyById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var crop_varietyList = await crop_VarietyRepository.GetCrop_VarietyById(Id);
                _logger.LogInformation($"database call done successfully with {crop_varietyList?.Id}");
                if (crop_varietyList == null)
                {
                    return NotFound();
                }
                return Ok(crop_varietyList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddCrop_Variety")]
        public async Task<IActionResult> CreateCrop_Variety(Crop_Variety Crop_VarietyDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var crop_Variety = new Crop_Variety()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var crop_varietyDTO = await crop_VarietyRepository.CreateCrop_Variety(Crop_VarietyDetails);
                _logger.LogInformation($"database call done successfully with {crop_varietyDTO?.Id}");
                return Ok(crop_varietyDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateCrop_Variety([FromRoute] int Id, [FromBody] Crop_Variety updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Crop_Variety crop_Variety = await crop_VarietyRepository.UpdateCrop_Variety(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {crop_Variety}");
                if (crop_Variety == null) 
                { 
                    return NotFound(); 
                }
                return Ok(crop_Variety);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateCrop_VarietyStatus/{Id:int}")]
        public async Task<IActionResult> UpdateCrop_VarietyStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Crop_Variety crop_Variety = await crop_VarietyRepository.UpdateCrop_VarietyStatus(Id);
                _logger.LogInformation($"database call done successfully with {crop_Variety}");
                if (crop_Variety == null) 
                { 
                    return NotFound(); 
                }
                return Ok(crop_Variety);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteCrop_Variety(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await crop_VarietyRepository.DeleteCrop_Variety(Id);
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

	  [HttpGet("~/GetCrop_VarietyLookup")]
        public async Task<IActionResult> GetCrop_VarietyLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var crop_varietyList = await crop_VarietyRepository.GetCrop_VarietyLookup();
                _logger.LogInformation($"database call done successfully with {crop_varietyList?.Count()}");
                return Ok(crop_varietyList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchCrop_Variety")]
        public async Task<IActionResult> SearchCrop_Variety(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var crop_varietyList = crop_VarietyRepository.SearchCrop_Variety(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {crop_varietyList?.Count()}");
                return Ok(crop_varietyList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
