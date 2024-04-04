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
    public class Poaching_FFBController : Controller
    {
        private readonly IPoaching_FFBRepository poaching_FFBRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Poaching_FFBController> _logger;
        public Poaching_FFBController(IPoaching_FFBRepository poaching_FFBRepository, IMapper mapper, ILogger<Poaching_FFBController> logger)
        {
            this.poaching_FFBRepository = poaching_FFBRepository;
            mapper = mapper;
            _logger = logger;
        }

        [HttpGet("~/GetAllPoaching_FFB")]
        public async Task<IActionResult> GetAllPoaching_FFB()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var poaching_ffbList = await poaching_FFBRepository.GetAllPoaching_FFB();
                _logger.LogInformation($"database call done successfully with {poaching_ffbList?.Count()}");
                return Ok(poaching_ffbList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetPoaching_FFBById")]
        public async Task<IActionResult> GetPoaching_FFBById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var poaching_ffbList = await poaching_FFBRepository.GetPoaching_FFBById(Id);
                _logger.LogInformation($"database call done successfully with {poaching_ffbList?.Id}");
                if (poaching_ffbList == null)
                {
                    return NotFound();
                }
                return Ok(poaching_ffbList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddPoaching_FFB")]
        public async Task<IActionResult> CreatePoaching_FFB(Poaching_FFB Poaching_FFBDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var poaching_FFB = new Poaching_FFB()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var poaching_ffbDTO = await poaching_FFBRepository.CreatePoaching_FFB(Poaching_FFBDetails);
                _logger.LogInformation($"database call done successfully with {poaching_ffbDTO?.Id}");
                return Ok(poaching_ffbDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdatePoaching_FFB([FromRoute] int Id, [FromBody] Poaching_FFB updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Poaching_FFB poaching_FFB = await poaching_FFBRepository.UpdatePoaching_FFB(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {poaching_FFB}");
                if (poaching_FFB == null) 
                { 
                    return NotFound(); 
                }
                return Ok(poaching_FFB);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdatePoaching_FFBStatus/{Id:int}")]
        public async Task<IActionResult> UpdatePoaching_FFBStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Poaching_FFB poaching_FFB = await poaching_FFBRepository.UpdatePoaching_FFBStatus(Id);
                _logger.LogInformation($"database call done successfully with {poaching_FFB}");
                if (poaching_FFB == null) 
                { 
                    return NotFound(); 
                }
                return Ok(poaching_FFB);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeletePoaching_FFB(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await poaching_FFBRepository.DeletePoaching_FFB(Id);
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

	  [HttpGet("~/GetPoaching_FFBLookup")]
        public async Task<IActionResult> GetPoaching_FFBLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var poaching_ffbList = await poaching_FFBRepository.GetPoaching_FFBLookup();
                _logger.LogInformation($"database call done successfully with {poaching_ffbList?.Count()}");
                return Ok(poaching_ffbList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchPoaching_FFB")]
        public async Task<IActionResult> SearchPoaching_FFB(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
                var poaching_ffbList = poaching_FFBRepository.SearchPoaching_FFB(searchText, pageNumber, pageSize, sortColumn, sortOrder);
                _logger.LogInformation($"database call done successfully with {poaching_ffbList?.Count()}");
                return Ok(poaching_ffbList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
