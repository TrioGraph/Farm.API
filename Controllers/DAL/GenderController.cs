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
    public class GenderController : Controller
    {
        private readonly IGenderRepository genderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GenderController> _logger;
	private IUtilityHelper utilityHelper;
        public GenderController(IGenderRepository genderRepository, IMapper mapper, ILogger<GenderController> logger,
	IUtilityHelper utilityHelper)
        {
            this.genderRepository = genderRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllGender")]
        public async Task<IActionResult> GetAllGender()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var genderList = await genderRepository.GetAllGender();
                _logger.LogInformation($"database call done successfully with {genderList?.Count()}");
                return Ok(genderList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetGenderById")]
        public async Task<IActionResult> GetGenderById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var genderList = await genderRepository.GetGenderById(Id);
                _logger.LogInformation($"database call done successfully with {genderList?.Id}");
                if (genderList == null)
                {
                    return NotFound();
                }
                return Ok(genderList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddGender")]
        public async Task<IActionResult> CreateGender(Gender GenderDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var gender = new Gender()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var genderDTO = await genderRepository.CreateGender(GenderDetails);
                _logger.LogInformation($"database call done successfully with {genderDTO?.Id}");
                return Ok(genderDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateGender([FromRoute] int Id, [FromBody] Gender updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Gender gender = await genderRepository.UpdateGender(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {gender}");
                if (gender == null) 
                { 
                    return NotFound(); 
                }
                return Ok(gender);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateGenderStatus/{Id:int}")]
        public async Task<IActionResult> UpdateGenderStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Gender gender = await genderRepository.UpdateGenderStatus(Id);
                _logger.LogInformation($"database call done successfully with {gender}");
                if (gender == null) 
                { 
                    return NotFound(); 
                }
                return Ok(gender);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteGender(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await genderRepository.DeleteGender(Id);
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

	  [HttpGet("~/GetGenderLookup")]
        public async Task<IActionResult> GetGenderLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var genderList = await genderRepository.GetGenderLookup();
                _logger.LogInformation($"database call done successfully with {genderList?.Count()}");
                return Ok(genderList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchGender")]
        public async Task<IActionResult> SearchGender(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var genderList = genderRepository.SearchGender(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {genderList?.Count()}");
                return Ok(genderList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
