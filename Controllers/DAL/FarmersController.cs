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
    public class FarmersController : Controller
    {
        private readonly IFarmersRepository farmersRepository;
        private readonly IMapper mapper;
        private readonly ILogger<FarmersController> _logger;
        public FarmersController(IFarmersRepository farmersRepository, IMapper mapper, ILogger<FarmersController> logger)
        {
            this.farmersRepository = farmersRepository;
            mapper = mapper;
            _logger = logger;
        }

        [HttpGet("~/GetAllFarmers")]
        public async Task<IActionResult> GetAllFarmers()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var farmersList = await farmersRepository.GetAllFarmers();
                _logger.LogInformation($"database call done successfully with {farmersList?.Count()}");
                return Ok(farmersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetFarmersById")]
        public async Task<IActionResult> GetFarmersById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var farmersList = await farmersRepository.GetFarmersById(Id);
                _logger.LogInformation($"database call done successfully with {farmersList?.Id}");
                if (farmersList == null)
                {
                    return NotFound();
                }
                return Ok(farmersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddFarmers")]
        public async Task<IActionResult> CreateFarmers(Farmers FarmersDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var farmers = new Farmers()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var farmersDTO = await farmersRepository.CreateFarmers(FarmersDetails);
                _logger.LogInformation($"database call done successfully with {farmersDTO?.Id}");
                return Ok(farmersDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateFarmers([FromRoute] int Id, [FromBody] Farmers updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farmers farmers = await farmersRepository.UpdateFarmers(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {farmers}");
                if (farmers == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmers);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateFarmersStatus/{Id:int}")]
        public async Task<IActionResult> UpdateFarmersStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farmers farmers = await farmersRepository.UpdateFarmersStatus(Id);
                _logger.LogInformation($"database call done successfully with {farmers}");
                if (farmers == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmers);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteFarmers(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await farmersRepository.DeleteFarmers(Id);
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

	  [HttpGet("~/GetFarmersLookup/{searchText}")]
        public async Task<IActionResult> GetFarmersLookup(string searchText)
        {
            try
            {
                _logger.LogInformation($"Start");
                var farmersList = await farmersRepository.GetFarmersLookup(searchText);
                _logger.LogInformation($"database call done successfully with {farmersList?.Count()}");
                return Ok(farmersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchFarmers")]
        public async Task<IActionResult> SearchFarmers(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
                var farmersList = farmersRepository.SearchFarmers(searchText, pageNumber, pageSize, sortColumn, sortOrder);
                _logger.LogInformation($"database call done successfully with {farmersList?.Count()}");
                return Ok(farmersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
