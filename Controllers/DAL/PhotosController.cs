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
    public class PhotosController : Controller
    {
        private readonly IPhotosRepository photosRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PhotosController> _logger;
	private IUtilityHelper utilityHelper;
        public PhotosController(IPhotosRepository photosRepository, IMapper mapper, ILogger<PhotosController> logger,
	IUtilityHelper utilityHelper)
        {
            this.photosRepository = photosRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllPhotos")]
        public async Task<IActionResult> GetAllPhotos()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var photosList = await photosRepository.GetAllPhotos();
                _logger.LogInformation($"database call done successfully with {photosList?.Count()}");
                return Ok(photosList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetPhotosById")]
        public async Task<IActionResult> GetPhotosById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var photosList = await photosRepository.GetPhotosById(Id);
                _logger.LogInformation($"database call done successfully with {photosList?.Id}");
                if (photosList == null)
                {
                    return NotFound();
                }
                return Ok(photosList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddPhotos")]
        public async Task<IActionResult> CreatePhotos(Photos PhotosDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var photos = new Photos()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var photosDTO = await photosRepository.CreatePhotos(PhotosDetails);
                _logger.LogInformation($"database call done successfully with {photosDTO?.Id}");
                return Ok(photosDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdatePhotos([FromRoute] int Id, [FromBody] Photos updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Photos photos = await photosRepository.UpdatePhotos(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {photos}");
                if (photos == null) 
                { 
                    return NotFound(); 
                }
                return Ok(photos);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdatePhotosStatus/{Id:int}")]
        public async Task<IActionResult> UpdatePhotosStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Photos photos = await photosRepository.UpdatePhotosStatus(Id);
                _logger.LogInformation($"database call done successfully with {photos}");
                if (photos == null) 
                { 
                    return NotFound(); 
                }
                return Ok(photos);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeletePhotos(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await photosRepository.DeletePhotos(Id);
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

	  [HttpGet("~/GetPhotosLookup")]
        public async Task<IActionResult> GetPhotosLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var photosList = await photosRepository.GetPhotosLookup();
                _logger.LogInformation($"database call done successfully with {photosList?.Count()}");
                return Ok(photosList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchPhotos")]
        public async Task<IActionResult> SearchPhotos(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var photosList = photosRepository.SearchPhotos(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {photosList?.Count()}");
                return Ok(photosList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
