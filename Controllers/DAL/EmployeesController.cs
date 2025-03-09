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
    public class EmployeesController : Controller
    {
        private readonly IEmployeesRepository employeesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<EmployeesController> _logger;
	private IUtilityHelper utilityHelper;
        public EmployeesController(IEmployeesRepository employeesRepository, IMapper mapper, ILogger<EmployeesController> logger,
	IUtilityHelper utilityHelper)
        {
            this.employeesRepository = employeesRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var employeesList = await employeesRepository.GetAllEmployees();
                _logger.LogInformation($"database call done successfully with {employeesList?.Count()}");
                return Ok(employeesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetEmployeesById")]
        public async Task<IActionResult> GetEmployeesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var employeesList = await employeesRepository.GetEmployeesById(Id);
                _logger.LogInformation($"database call done successfully with {employeesList?.Id}");
                if (employeesList == null)
                {
                    return NotFound();
                }
                return Ok(employeesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddEmployees")]
        public async Task<IActionResult> CreateEmployees(Employees EmployeesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var employees = new Employees()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var employeesDTO = await employeesRepository.CreateEmployees(EmployeesDetails);
                _logger.LogInformation($"database call done successfully with {employeesDTO?.Id}");
                return Ok(employeesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateEmployees([FromRoute] int Id, [FromBody] Employees updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Employees employees = await employeesRepository.UpdateEmployees(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {employees}");
                if (employees == null) 
                { 
                    return NotFound(); 
                }
                return Ok(employees);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateEmployeesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateEmployeesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Employees employees = await employeesRepository.UpdateEmployeesStatus(Id);
                _logger.LogInformation($"database call done successfully with {employees}");
                if (employees == null) 
                { 
                    return NotFound(); 
                }
                return Ok(employees);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteEmployees(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await employeesRepository.DeleteEmployees(Id);
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

	  [HttpGet("~/GetEmployeesLookup")]
        public async Task<IActionResult> GetEmployeesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var employeesList = await employeesRepository.GetEmployeesLookup();
                _logger.LogInformation($"database call done successfully with {employeesList?.Count()}");
                return Ok(employeesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchEmployees")]
        public async Task<IActionResult> SearchEmployees(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var employeesList = employeesRepository.SearchEmployees(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {employeesList?.Count()}");
                return Ok(employeesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
