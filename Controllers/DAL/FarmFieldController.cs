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
    public class FarmFieldController : Controller
    {
        private readonly IFarmFieldRepository farmFieldRepository;
        private readonly IMapper mapper;
        private readonly ILogger<FarmFieldController> _logger;
        public FarmFieldController(IFarmFieldRepository farmFieldRepository, IMapper mapper, ILogger<FarmFieldController> logger)
        {
            this.farmFieldRepository = farmFieldRepository;
            mapper = mapper;
            _logger = logger;
        }

        [HttpGet("~/GetAllFarmField")]
        public async Task<IActionResult> GetAllFarmField()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var farmfieldList = await farmFieldRepository.GetAllFarmField();
                _logger.LogInformation($"database call done successfully with {farmfieldList?.Count()}");
                return Ok(farmfieldList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetFarmFieldById")]
        public async Task<IActionResult> GetFarmFieldById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var farmfieldList = await farmFieldRepository.GetFarmFieldById(Id);
                _logger.LogInformation($"database call done successfully with {farmfieldList?.Id}");
                if (farmfieldList == null)
                {
                    return NotFound();
                }
                return Ok(farmfieldList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddFarmField")]
        public async Task<IActionResult> CreateFarmField(FarmField FarmFieldDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var farmField = new FarmField()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var farmfieldDTO = await farmFieldRepository.CreateFarmField(FarmFieldDetails);
                _logger.LogInformation($"database call done successfully with {farmfieldDTO?.Id}");
                return Ok(farmfieldDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateFarmField([FromRoute] int Id, [FromBody] FarmField updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                FarmField farmField = await farmFieldRepository.UpdateFarmField(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {farmField}");
                if (farmField == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmField);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateFarmFieldStatus/{Id:int}")]
        public async Task<IActionResult> UpdateFarmFieldStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                FarmField farmField = await farmFieldRepository.UpdateFarmFieldStatus(Id);
                _logger.LogInformation($"database call done successfully with {farmField}");
                if (farmField == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmField);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteFarmField(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await farmFieldRepository.DeleteFarmField(Id);
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

	  [HttpGet("~/GetFarmFieldLookup")]
        public async Task<IActionResult> GetFarmFieldLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var farmfieldList = await farmFieldRepository.GetFarmFieldLookup();
                _logger.LogInformation($"database call done successfully with {farmfieldList?.Count()}");
                return Ok(farmfieldList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchFarmField")]
        public async Task<IActionResult> SearchFarmField(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
                var farmfieldList = farmFieldRepository.SearchFarmField(searchText, pageNumber, pageSize, sortColumn, sortOrder);
                _logger.LogInformation($"database call done successfully with {farmfieldList?.Count()}");
                return Ok(farmfieldList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
