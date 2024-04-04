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
    public class Mandal_BlocksController : Controller
    {
        private readonly IMandal_BlocksRepository mandal_BlocksRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Mandal_BlocksController> _logger;
        public Mandal_BlocksController(IMandal_BlocksRepository mandal_BlocksRepository, IMapper mapper, ILogger<Mandal_BlocksController> logger)
        {
            this.mandal_BlocksRepository = mandal_BlocksRepository;
            mapper = mapper;
            _logger = logger;
        }

        [HttpGet("~/GetAllMandal_Blocks")]
        public async Task<IActionResult> GetAllMandal_Blocks()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var mandal_blocksList = await mandal_BlocksRepository.GetAllMandal_Blocks();
                _logger.LogInformation($"database call done successfully with {mandal_blocksList?.Count()}");
                return Ok(mandal_blocksList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetMandal_BlocksById")]
        public async Task<IActionResult> GetMandal_BlocksById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var mandal_blocksList = await mandal_BlocksRepository.GetMandal_BlocksById(Id);
                _logger.LogInformation($"database call done successfully with {mandal_blocksList?.Id}");
                if (mandal_blocksList == null)
                {
                    return NotFound();
                }
                return Ok(mandal_blocksList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddMandal_Blocks")]
        public async Task<IActionResult> CreateMandal_Blocks(Mandal_Blocks Mandal_BlocksDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var mandal_Blocks = new Mandal_Blocks()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var mandal_blocksDTO = await mandal_BlocksRepository.CreateMandal_Blocks(Mandal_BlocksDetails);
                _logger.LogInformation($"database call done successfully with {mandal_blocksDTO?.Id}");
                return Ok(mandal_blocksDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateMandal_Blocks([FromRoute] int Id, [FromBody] Mandal_Blocks updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Mandal_Blocks mandal_Blocks = await mandal_BlocksRepository.UpdateMandal_Blocks(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {mandal_Blocks}");
                if (mandal_Blocks == null) 
                { 
                    return NotFound(); 
                }
                return Ok(mandal_Blocks);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateMandal_BlocksStatus/{Id:int}")]
        public async Task<IActionResult> UpdateMandal_BlocksStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Mandal_Blocks mandal_Blocks = await mandal_BlocksRepository.UpdateMandal_BlocksStatus(Id);
                _logger.LogInformation($"database call done successfully with {mandal_Blocks}");
                if (mandal_Blocks == null) 
                { 
                    return NotFound(); 
                }
                return Ok(mandal_Blocks);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteMandal_Blocks(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await mandal_BlocksRepository.DeleteMandal_Blocks(Id);
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

	  [HttpGet("~/GetMandal_BlocksLookup")]
        public async Task<IActionResult> GetMandal_BlocksLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var mandal_blocksList = await mandal_BlocksRepository.GetMandal_BlocksLookup();
                _logger.LogInformation($"database call done successfully with {mandal_blocksList?.Count()}");
                return Ok(mandal_blocksList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchMandal_Blocks")]
        public async Task<IActionResult> SearchMandal_Blocks(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
                var mandal_blocksList = mandal_BlocksRepository.SearchMandal_Blocks(searchText, pageNumber, pageSize, sortColumn, sortOrder);
                _logger.LogInformation($"database call done successfully with {mandal_blocksList?.Count()}");
                return Ok(mandal_blocksList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
