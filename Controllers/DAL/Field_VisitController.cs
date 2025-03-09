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
    public class Field_VisitController : Controller
    {
        private readonly IField_VisitRepository field_VisitRepository;
        private IUtilityHelper utilityHelper;
        private readonly IMapper mapper;
        private readonly ILogger<Field_VisitController> _logger;
        public Field_VisitController(IField_VisitRepository field_VisitRepository, IMapper mapper, ILogger<Field_VisitController> logger, IUtilityHelper utilityHelper)
        {
            this.field_VisitRepository = field_VisitRepository;
            mapper = mapper;
            _logger = logger;
            this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllField_Visit")]
        public async Task<IActionResult> GetAllField_Visit()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var field_visitList = await field_VisitRepository.GetAllField_Visit();
                _logger.LogInformation($"database call done successfully with {field_visitList?.Count()}");
                return Ok(field_visitList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetField_VisitById")]
        public async Task<IActionResult> GetField_VisitById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var field_visitList = await field_VisitRepository.GetField_VisitById(Id);
                _logger.LogInformation($"database call done successfully with {field_visitList?.Id}");
                if (field_visitList == null)
                {
                    return NotFound();
                }
                return Ok(field_visitList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddField_Visit")]
        public async Task<IActionResult> CreateField_Visit(Field_Visit Field_VisitDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var field_Visit = new Field_Visit()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var field_visitDTO = await field_VisitRepository.CreateField_Visit(Field_VisitDetails);
                _logger.LogInformation($"database call done successfully with {field_visitDTO?.Id}");
                return Ok(field_visitDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateField_Visit([FromRoute] int Id, [FromBody] Field_Visit updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Field_Visit field_Visit = await field_VisitRepository.UpdateField_Visit(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {field_Visit}");
                if (field_Visit == null) 
                { 
                    return NotFound(); 
                }
                return Ok(field_Visit);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateField_VisitStatus/{Id:int}")]
        public async Task<IActionResult> UpdateField_VisitStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Field_Visit field_Visit = await field_VisitRepository.UpdateField_VisitStatus(Id);
                _logger.LogInformation($"database call done successfully with {field_Visit}");
                if (field_Visit == null) 
                { 
                    return NotFound(); 
                }
                return Ok(field_Visit);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteField_Visit(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await field_VisitRepository.DeleteField_Visit(Id);
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

	  [HttpGet("~/GetField_VisitLookup")]
        public async Task<IActionResult> GetField_VisitLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var field_visitList = await field_VisitRepository.GetField_VisitLookup();
                _logger.LogInformation($"database call done successfully with {field_visitList?.Count()}");
                return Ok(field_visitList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchField_Visit")]
        public async Task<IActionResult> SearchField_Visit(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
                string userId = utilityHelper.GetUserFromRequest(Request);
                var field_visitList = field_VisitRepository.SearchField_Visit(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder);
                _logger.LogInformation($"database call done successfully with {field_visitList?.Count()}");
                return Ok(field_visitList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
