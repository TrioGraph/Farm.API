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
    public class RolesController : Controller
    {
        private readonly IRolesRepository rolesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RolesController> _logger;
	private IUtilityHelper utilityHelper;
        public RolesController(IRolesRepository rolesRepository, IMapper mapper, ILogger<RolesController> logger,
	IUtilityHelper utilityHelper)
        {
            this.rolesRepository = rolesRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var rolesList = await rolesRepository.GetAllRoles();
                _logger.LogInformation($"database call done successfully with {rolesList?.Count()}");
                return Ok(rolesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetRolesById")]
        public async Task<IActionResult> GetRolesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var rolesList = await rolesRepository.GetRolesById(Id);
                _logger.LogInformation($"database call done successfully with {rolesList?.Id}");
                if (rolesList == null)
                {
                    return NotFound();
                }
                return Ok(rolesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddRoles")]
        public async Task<IActionResult> CreateRoles(Roles RolesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var roles = new Roles()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var rolesDTO = await rolesRepository.CreateRoles(RolesDetails);
                _logger.LogInformation($"database call done successfully with {rolesDTO?.Id}");
                return Ok(rolesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateRoles([FromRoute] int Id, [FromBody] Roles updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Roles roles = await rolesRepository.UpdateRoles(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {roles}");
                if (roles == null) 
                { 
                    return NotFound(); 
                }
                return Ok(roles);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateRolesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateRolesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Roles roles = await rolesRepository.UpdateRolesStatus(Id);
                _logger.LogInformation($"database call done successfully with {roles}");
                if (roles == null) 
                { 
                    return NotFound(); 
                }
                return Ok(roles);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteRoles(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await rolesRepository.DeleteRoles(Id);
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

	  [HttpGet("~/GetRolesLookup")]
        public async Task<IActionResult> GetRolesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var rolesList = await rolesRepository.GetRolesLookup();
                _logger.LogInformation($"database call done successfully with {rolesList?.Count()}");
                return Ok(rolesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchRoles")]
        public async Task<IActionResult> SearchRoles(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var rolesList = rolesRepository.SearchRoles(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {rolesList?.Count()}");
                return Ok(rolesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
