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
    public class Nursary_BatchesController : Controller
    {
        private readonly INursary_BatchesRepository nursary_BatchesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Nursary_BatchesController> _logger;
        public Nursary_BatchesController(INursary_BatchesRepository nursary_BatchesRepository, IMapper mapper, ILogger<Nursary_BatchesController> logger)
        {
            this.nursary_BatchesRepository = nursary_BatchesRepository;
            mapper = mapper;
            _logger = logger;
        }

        [HttpGet("~/GetAllNursary_Batches")]
        public async Task<IActionResult> GetAllNursary_Batches()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var nursary_batchesList = await nursary_BatchesRepository.GetAllNursary_Batches();
                _logger.LogInformation($"database call done successfully with {nursary_batchesList?.Count()}");
                return Ok(nursary_batchesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetNursary_BatchesById")]
        public async Task<IActionResult> GetNursary_BatchesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var nursary_batchesList = await nursary_BatchesRepository.GetNursary_BatchesById(Id);
                _logger.LogInformation($"database call done successfully with {nursary_batchesList?.Id}");
                if (nursary_batchesList == null)
                {
                    return NotFound();
                }
                return Ok(nursary_batchesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddNursary_Batches")]
        public async Task<IActionResult> CreateNursary_Batches(Nursary_Batches Nursary_BatchesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var nursary_Batches = new Nursary_Batches()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var nursary_batchesDTO = await nursary_BatchesRepository.CreateNursary_Batches(Nursary_BatchesDetails);
                _logger.LogInformation($"database call done successfully with {nursary_batchesDTO?.Id}");
                return Ok(nursary_batchesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateNursary_Batches([FromRoute] int Id, [FromBody] Nursary_Batches updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Nursary_Batches nursary_Batches = await nursary_BatchesRepository.UpdateNursary_Batches(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {nursary_Batches}");
                if (nursary_Batches == null) 
                { 
                    return NotFound(); 
                }
                return Ok(nursary_Batches);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateNursary_BatchesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateNursary_BatchesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Nursary_Batches nursary_Batches = await nursary_BatchesRepository.UpdateNursary_BatchesStatus(Id);
                _logger.LogInformation($"database call done successfully with {nursary_Batches}");
                if (nursary_Batches == null) 
                { 
                    return NotFound(); 
                }
                return Ok(nursary_Batches);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteNursary_Batches(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await nursary_BatchesRepository.DeleteNursary_Batches(Id);
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

	  [HttpGet("~/GetNursary_BatchesLookup")]
        public async Task<IActionResult> GetNursary_BatchesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var nursary_batchesList = await nursary_BatchesRepository.GetNursary_BatchesLookup();
                _logger.LogInformation($"database call done successfully with {nursary_batchesList?.Count()}");
                return Ok(nursary_batchesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchNursary_Batches")]
        public async Task<IActionResult> SearchNursary_Batches(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
                var nursary_batchesList = nursary_BatchesRepository.SearchNursary_Batches(searchText, pageNumber, pageSize, sortColumn, sortOrder);
                _logger.LogInformation($"database call done successfully with {nursary_batchesList?.Count()}");
                return Ok(nursary_batchesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
