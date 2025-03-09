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
    public class Farm_DiseasesController : Controller
    {
        private readonly IFarm_DiseasesRepository farm_DiseasesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Farm_DiseasesController> _logger;
	private IUtilityHelper utilityHelper;
        public Farm_DiseasesController(IFarm_DiseasesRepository farm_DiseasesRepository, IMapper mapper, ILogger<Farm_DiseasesController> logger,
	IUtilityHelper utilityHelper)
        {
            this.farm_DiseasesRepository = farm_DiseasesRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllFarm_Diseases")]
        public async Task<IActionResult> GetAllFarm_Diseases()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var farm_diseasesList = await farm_DiseasesRepository.GetAllFarm_Diseases();
                _logger.LogInformation($"database call done successfully with {farm_diseasesList?.Count()}");
                return Ok(farm_diseasesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetFarm_DiseasesById")]
        public async Task<IActionResult> GetFarm_DiseasesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var farm_diseasesList = await farm_DiseasesRepository.GetFarm_DiseasesById(Id);
                _logger.LogInformation($"database call done successfully with {farm_diseasesList?.Id}");
                if (farm_diseasesList == null)
                {
                    return NotFound();
                }
                return Ok(farm_diseasesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddFarm_Diseases")]
        public async Task<IActionResult> CreateFarm_Diseases(Farm_Diseases Farm_DiseasesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var farm_Diseases = new Farm_Diseases()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var farm_diseasesDTO = await farm_DiseasesRepository.CreateFarm_Diseases(Farm_DiseasesDetails);
                _logger.LogInformation($"database call done successfully with {farm_diseasesDTO?.Id}");
                return Ok(farm_diseasesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateFarm_Diseases([FromRoute] int Id, [FromBody] Farm_Diseases updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farm_Diseases farm_Diseases = await farm_DiseasesRepository.UpdateFarm_Diseases(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {farm_Diseases}");
                if (farm_Diseases == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farm_Diseases);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateFarm_DiseasesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateFarm_DiseasesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farm_Diseases farm_Diseases = await farm_DiseasesRepository.UpdateFarm_DiseasesStatus(Id);
                _logger.LogInformation($"database call done successfully with {farm_Diseases}");
                if (farm_Diseases == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farm_Diseases);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteFarm_Diseases(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await farm_DiseasesRepository.DeleteFarm_Diseases(Id);
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

	  [HttpGet("~/GetFarm_DiseasesLookup")]
        public async Task<IActionResult> GetFarm_DiseasesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var farm_diseasesList = await farm_DiseasesRepository.GetFarm_DiseasesLookup();
                _logger.LogInformation($"database call done successfully with {farm_diseasesList?.Count()}");
                return Ok(farm_diseasesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchFarm_Diseases")]
        public async Task<IActionResult> SearchFarm_Diseases(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var farm_diseasesList = farm_DiseasesRepository.SearchFarm_Diseases(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {farm_diseasesList?.Count()}");
                return Ok(farm_diseasesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
