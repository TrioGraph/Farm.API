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
    public class Role_PrivilegesController : Controller
    {
        private readonly IRole_PrivilegesRepository role_PrivilegesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Role_PrivilegesController> _logger;
	private IUtilityHelper utilityHelper;
        public Role_PrivilegesController(IRole_PrivilegesRepository role_PrivilegesRepository, IMapper mapper, ILogger<Role_PrivilegesController> logger,
	IUtilityHelper utilityHelper)
        {
            this.role_PrivilegesRepository = role_PrivilegesRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllRole_Privileges")]
        public async Task<IActionResult> GetAllRole_Privileges()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var role_privilegesList = await role_PrivilegesRepository.GetAllRole_Privileges();
                _logger.LogInformation($"database call done successfully with {role_privilegesList?.Count()}");
                return Ok(role_privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetRole_PrivilegesById")]
        public async Task<IActionResult> GetRole_PrivilegesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var role_privilegesList = await role_PrivilegesRepository.GetRole_PrivilegesById(Id);
                _logger.LogInformation($"database call done successfully with {role_privilegesList?.Id}");
                if (role_privilegesList == null)
                {
                    return NotFound();
                }
                return Ok(role_privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddRole_Privileges")]
        public async Task<IActionResult> CreateRole_Privileges(Role_Privileges Role_PrivilegesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var role_Privileges = new Role_Privileges()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var role_privilegesDTO = await role_PrivilegesRepository.CreateRole_Privileges(Role_PrivilegesDetails);
                _logger.LogInformation($"database call done successfully with {role_privilegesDTO?.Id}");
                return Ok(role_privilegesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateRole_Privileges([FromRoute] int Id, [FromBody] Role_Privileges updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Role_Privileges role_Privileges = await role_PrivilegesRepository.UpdateRole_Privileges(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {role_Privileges}");
                if (role_Privileges == null) 
                { 
                    return NotFound(); 
                }
                return Ok(role_Privileges);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateRole_PrivilegesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateRole_PrivilegesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Role_Privileges role_Privileges = await role_PrivilegesRepository.UpdateRole_PrivilegesStatus(Id);
                _logger.LogInformation($"database call done successfully with {role_Privileges}");
                if (role_Privileges == null) 
                { 
                    return NotFound(); 
                }
                return Ok(role_Privileges);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteRole_Privileges(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await role_PrivilegesRepository.DeleteRole_Privileges(Id);
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

	  [HttpGet("~/GetRole_PrivilegesLookup")]
        public async Task<IActionResult> GetRole_PrivilegesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var role_privilegesList = await role_PrivilegesRepository.GetRole_PrivilegesLookup();
                _logger.LogInformation($"database call done successfully with {role_privilegesList?.Count()}");
                return Ok(role_privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchRole_Privileges")]
        public async Task<IActionResult> SearchRole_Privileges(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var role_privilegesList = role_PrivilegesRepository.SearchRole_Privileges(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {role_privilegesList?.Count()}");
                return Ok(role_privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
