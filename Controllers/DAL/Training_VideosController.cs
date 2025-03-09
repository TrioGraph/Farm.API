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
    public class Training_VideosController : Controller
    {
        private readonly ITraining_VideosRepository training_VideosRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Training_VideosController> _logger;
	private IUtilityHelper utilityHelper;
        public Training_VideosController(ITraining_VideosRepository training_VideosRepository, IMapper mapper, ILogger<Training_VideosController> logger,
	IUtilityHelper utilityHelper)
        {
            this.training_VideosRepository = training_VideosRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllTraining_Videos")]
        public async Task<IActionResult> GetAllTraining_Videos()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var training_videosList = await training_VideosRepository.GetAllTraining_Videos();
                _logger.LogInformation($"database call done successfully with {training_videosList?.Count()}");
                return Ok(training_videosList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetTraining_VideosById")]
        public async Task<IActionResult> GetTraining_VideosById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var training_videosList = await training_VideosRepository.GetTraining_VideosById(Id);
                _logger.LogInformation($"database call done successfully with {training_videosList?.Id}");
                if (training_videosList == null)
                {
                    return NotFound();
                }
                return Ok(training_videosList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddTraining_Videos")]
        public async Task<IActionResult> CreateTraining_Videos(Training_Videos Training_VideosDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var training_Videos = new Training_Videos()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var training_videosDTO = await training_VideosRepository.CreateTraining_Videos(Training_VideosDetails);
                _logger.LogInformation($"database call done successfully with {training_videosDTO?.Id}");
                return Ok(training_videosDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateTraining_Videos([FromRoute] int Id, [FromBody] Training_Videos updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Training_Videos training_Videos = await training_VideosRepository.UpdateTraining_Videos(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {training_Videos}");
                if (training_Videos == null) 
                { 
                    return NotFound(); 
                }
                return Ok(training_Videos);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateTraining_VideosStatus/{Id:int}")]
        public async Task<IActionResult> UpdateTraining_VideosStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Training_Videos training_Videos = await training_VideosRepository.UpdateTraining_VideosStatus(Id);
                _logger.LogInformation($"database call done successfully with {training_Videos}");
                if (training_Videos == null) 
                { 
                    return NotFound(); 
                }
                return Ok(training_Videos);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteTraining_Videos(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await training_VideosRepository.DeleteTraining_Videos(Id);
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

	  [HttpGet("~/GetTraining_VideosLookup")]
        public async Task<IActionResult> GetTraining_VideosLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var training_videosList = await training_VideosRepository.GetTraining_VideosLookup();
                _logger.LogInformation($"database call done successfully with {training_videosList?.Count()}");
                return Ok(training_videosList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchTraining_Videos")]
        public async Task<IActionResult> SearchTraining_Videos(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var training_videosList = training_VideosRepository.SearchTraining_Videos(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {training_videosList?.Count()}");
                return Ok(training_videosList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
