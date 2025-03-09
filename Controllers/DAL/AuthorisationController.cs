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
    public class AuthorisationController : Controller
    {
        private readonly IAuthorisationRepository authorisationRepository;
        private readonly IMapper mapper;
        private readonly ILogger<AuthorisationController> _logger;
	private IUtilityHelper utilityHelper;
        public AuthorisationController(IAuthorisationRepository authorisationRepository, IMapper mapper, ILogger<AuthorisationController> logger,
	IUtilityHelper utilityHelper)
        {
            this.authorisationRepository = authorisationRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllAuthorisation")]
        public async Task<IActionResult> GetAllAuthorisation()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var authorisationList = await authorisationRepository.GetAllAuthorisation();
                _logger.LogInformation($"database call done successfully with {authorisationList?.Count()}");
                return Ok(authorisationList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetAuthorisationById")]
        public async Task<IActionResult> GetAuthorisationById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var authorisationList = await authorisationRepository.GetAuthorisationById(Id);
                _logger.LogInformation($"database call done successfully with {authorisationList?.Id}");
                if (authorisationList == null)
                {
                    return NotFound();
                }
                return Ok(authorisationList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddAuthorisation")]
        public async Task<IActionResult> CreateAuthorisation(Authorisation AuthorisationDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var authorisation = new Authorisation()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var authorisationDTO = await authorisationRepository.CreateAuthorisation(AuthorisationDetails);
                _logger.LogInformation($"database call done successfully with {authorisationDTO?.Id}");
                return Ok(authorisationDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateAuthorisation([FromRoute] int Id, [FromBody] Authorisation updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Authorisation authorisation = await authorisationRepository.UpdateAuthorisation(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {authorisation}");
                if (authorisation == null) 
                { 
                    return NotFound(); 
                }
                return Ok(authorisation);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateAuthorisationStatus/{Id:int}")]
        public async Task<IActionResult> UpdateAuthorisationStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Authorisation authorisation = await authorisationRepository.UpdateAuthorisationStatus(Id);
                _logger.LogInformation($"database call done successfully with {authorisation}");
                if (authorisation == null) 
                { 
                    return NotFound(); 
                }
                return Ok(authorisation);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteAuthorisation(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await authorisationRepository.DeleteAuthorisation(Id);
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

	  [HttpGet("~/GetAuthorisationLookup")]
        public async Task<IActionResult> GetAuthorisationLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var authorisationList = await authorisationRepository.GetAuthorisationLookup();
                _logger.LogInformation($"database call done successfully with {authorisationList?.Count()}");
                return Ok(authorisationList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchAuthorisation")]
        public async Task<IActionResult> SearchAuthorisation(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var authorisationList = authorisationRepository.SearchAuthorisation(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {authorisationList?.Count()}");
                return Ok(authorisationList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
