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
    public class NursaryController : Controller
    {
        private readonly INursaryRepository nursaryRepository;
        private readonly IMapper mapper;
        private readonly ILogger<NursaryController> _logger;
        public NursaryController(INursaryRepository nursaryRepository, IMapper mapper, ILogger<NursaryController> logger)
        {
            this.nursaryRepository = nursaryRepository;
            mapper = mapper;
            _logger = logger;
        }

        [HttpGet("~/GetAllNursary")]
        public async Task<IActionResult> GetAllNursary()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var nursaryList = await nursaryRepository.GetAllNursary();
                _logger.LogInformation($"database call done successfully with {nursaryList?.Count()}");
                return Ok(nursaryList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetNursaryById")]
        public async Task<IActionResult> GetNursaryById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var nursaryList = await nursaryRepository.GetNursaryById(Id);
                _logger.LogInformation($"database call done successfully with {nursaryList?.Id}");
                if (nursaryList == null)
                {
                    return NotFound();
                }
                return Ok(nursaryList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddNursary")]
        public async Task<IActionResult> CreateNursary(Nursary NursaryDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var nursary = new Nursary()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var nursaryDTO = await nursaryRepository.CreateNursary(NursaryDetails);
                _logger.LogInformation($"database call done successfully with {nursaryDTO?.Id}");
                return Ok(nursaryDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateNursary([FromRoute] int Id, [FromBody] Nursary updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Nursary nursary = await nursaryRepository.UpdateNursary(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {nursary}");
                if (nursary == null) 
                { 
                    return NotFound(); 
                }
                return Ok(nursary);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateNursaryStatus/{Id:int}")]
        public async Task<IActionResult> UpdateNursaryStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Nursary nursary = await nursaryRepository.UpdateNursaryStatus(Id);
                _logger.LogInformation($"database call done successfully with {nursary}");
                if (nursary == null) 
                { 
                    return NotFound(); 
                }
                return Ok(nursary);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteNursary(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await nursaryRepository.DeleteNursary(Id);
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

	  [HttpGet("~/GetNursaryLookup")]
        public async Task<IActionResult> GetNursaryLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var nursaryList = await nursaryRepository.GetNursaryLookup();
                _logger.LogInformation($"database call done successfully with {nursaryList?.Count()}");
                return Ok(nursaryList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchNursary")]
        public async Task<IActionResult> SearchNursary(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
                var nursaryList = nursaryRepository.SearchNursary(searchText, pageNumber, pageSize, sortColumn, sortOrder);
                _logger.LogInformation($"database call done successfully with {nursaryList?.Count()}");
                return Ok(nursaryList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
