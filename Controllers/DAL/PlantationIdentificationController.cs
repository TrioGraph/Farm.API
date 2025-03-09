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
    public class PlantationIdentificationController : Controller
    {
        private readonly IPlantationIdentificationRepository plantationIdentificationRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PlantationIdentificationController> _logger;

        private IUtilityHelper utilityHelper;
        public PlantationIdentificationController(IPlantationIdentificationRepository plantationIdentificationRepository, IMapper mapper, ILogger<PlantationIdentificationController> logger,
        IUtilityHelper utilityHelper)
        {
            this.plantationIdentificationRepository = plantationIdentificationRepository;
            mapper = mapper;
            _logger = logger;
            this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllPlantationIdentification")]
        public async Task<IActionResult> GetAllPlantationIdentification()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var plantationidentificationList = await plantationIdentificationRepository.GetAllPlantationIdentification();
                _logger.LogInformation($"database call done successfully with {plantationidentificationList?.Count()}");
                return Ok(plantationidentificationList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetPlantationIdentificationById")]
        public async Task<IActionResult> GetPlantationIdentificationById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var plantationidentificationList = await plantationIdentificationRepository.GetPlantationIdentificationById(Id);
                _logger.LogInformation($"database call done successfully with {plantationidentificationList?.Id}");
                if (plantationidentificationList == null)
                {
                    return NotFound();
                }
                return Ok(plantationidentificationList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddPlantationIdentification")]
        public async Task<IActionResult> CreatePlantationIdentification(PlantationIdentification PlantationIdentificationDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var plantationIdentification = new PlantationIdentification()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var plantationidentificationDTO = await plantationIdentificationRepository.CreatePlantationIdentification(PlantationIdentificationDetails);
                _logger.LogInformation($"database call done successfully with {plantationidentificationDTO?.Id}");
                return Ok(plantationidentificationDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdatePlantationIdentification([FromRoute] int Id, [FromBody] PlantationIdentification updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                PlantationIdentification plantationIdentification = await plantationIdentificationRepository.UpdatePlantationIdentification(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {plantationIdentification}");
                if (plantationIdentification == null) 
                { 
                    return NotFound(); 
                }
                return Ok(plantationIdentification);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdatePlantationIdentificationStatus/{Id:int}")]
        public async Task<IActionResult> UpdatePlantationIdentificationStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                PlantationIdentification plantationIdentification = await plantationIdentificationRepository.UpdatePlantationIdentificationStatus(Id);
                _logger.LogInformation($"database call done successfully with {plantationIdentification}");
                if (plantationIdentification == null) 
                { 
                    return NotFound(); 
                }
                return Ok(plantationIdentification);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeletePlantationIdentification(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await plantationIdentificationRepository.DeletePlantationIdentification(Id);
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

	  [HttpGet("~/GetPlantationIdentificationLookup")]
        public async Task<IActionResult> GetPlantationIdentificationLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var plantationidentificationList = await plantationIdentificationRepository.GetPlantationIdentificationLookup();
                _logger.LogInformation($"database call done successfully with {plantationidentificationList?.Count()}");
                return Ok(plantationidentificationList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchPlantationIdentification")]
        public async Task<IActionResult> SearchPlantationIdentification(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var plantationidentificationList = plantationIdentificationRepository.
                        SearchPlantationIdentification(int.Parse(userId),
                        searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {plantationidentificationList?.Count()}");
                return Ok(plantationidentificationList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
