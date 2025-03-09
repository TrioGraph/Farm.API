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
    public class Employee_RolesController : Controller
    {
        private readonly IEmployee_RolesRepository employee_RolesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Employee_RolesController> _logger;
	private IUtilityHelper utilityHelper;
        public Employee_RolesController(IEmployee_RolesRepository employee_RolesRepository, IMapper mapper, ILogger<Employee_RolesController> logger,
	IUtilityHelper utilityHelper)
        {
            this.employee_RolesRepository = employee_RolesRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllEmployee_Roles")]
        public async Task<IActionResult> GetAllEmployee_Roles()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var employee_rolesList = await employee_RolesRepository.GetAllEmployee_Roles();
                _logger.LogInformation($"database call done successfully with {employee_rolesList?.Count()}");
                return Ok(employee_rolesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetEmployee_RolesById")]
        public async Task<IActionResult> GetEmployee_RolesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var employee_rolesList = await employee_RolesRepository.GetEmployee_RolesById(Id);
                _logger.LogInformation($"database call done successfully with {employee_rolesList?.Id}");
                if (employee_rolesList == null)
                {
                    return NotFound();
                }
                return Ok(employee_rolesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddEmployee_Roles")]
        public async Task<IActionResult> CreateEmployee_Roles(Employee_Roles Employee_RolesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var employee_Roles = new Employee_Roles()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var employee_rolesDTO = await employee_RolesRepository.CreateEmployee_Roles(Employee_RolesDetails);
                _logger.LogInformation($"database call done successfully with {employee_rolesDTO?.Id}");
                return Ok(employee_rolesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateEmployee_Roles([FromRoute] int Id, [FromBody] Employee_Roles updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Employee_Roles employee_Roles = await employee_RolesRepository.UpdateEmployee_Roles(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {employee_Roles}");
                if (employee_Roles == null) 
                { 
                    return NotFound(); 
                }
                return Ok(employee_Roles);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateEmployee_RolesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateEmployee_RolesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Employee_Roles employee_Roles = await employee_RolesRepository.UpdateEmployee_RolesStatus(Id);
                _logger.LogInformation($"database call done successfully with {employee_Roles}");
                if (employee_Roles == null) 
                { 
                    return NotFound(); 
                }
                return Ok(employee_Roles);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteEmployee_Roles(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await employee_RolesRepository.DeleteEmployee_Roles(Id);
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

	  [HttpGet("~/GetEmployee_RolesLookup")]
        public async Task<IActionResult> GetEmployee_RolesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var employee_rolesList = await employee_RolesRepository.GetEmployee_RolesLookup();
                _logger.LogInformation($"database call done successfully with {employee_rolesList?.Count()}");
                return Ok(employee_rolesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchEmployee_Roles")]
        public async Task<IActionResult> SearchEmployee_Roles(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var employee_rolesList = employee_RolesRepository.SearchEmployee_Roles(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {employee_rolesList?.Count()}");
                return Ok(employee_rolesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
