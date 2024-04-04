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
    public class Employee_TypesController : Controller
    {
        private readonly IEmployee_TypesRepository employee_TypesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Employee_TypesController> _logger;
        public Employee_TypesController(IEmployee_TypesRepository employee_TypesRepository, IMapper mapper, ILogger<Employee_TypesController> logger)
        {
            this.employee_TypesRepository = employee_TypesRepository;
            mapper = mapper;
            _logger = logger;
        }

        [HttpGet("~/GetAllEmployee_Types")]
        public async Task<IActionResult> GetAllEmployee_Types()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var employee_typesList = await employee_TypesRepository.GetAllEmployee_Types();
                _logger.LogInformation($"database call done successfully with {employee_typesList?.Count()}");
                return Ok(employee_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetEmployee_TypesById")]
        public async Task<IActionResult> GetEmployee_TypesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var employee_typesList = await employee_TypesRepository.GetEmployee_TypesById(Id);
                _logger.LogInformation($"database call done successfully with {employee_typesList?.Id}");
                if (employee_typesList == null)
                {
                    return NotFound();
                }
                return Ok(employee_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddEmployee_Types")]
        public async Task<IActionResult> CreateEmployee_Types(Employee_Types Employee_TypesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var employee_Types = new Employee_Types()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var employee_typesDTO = await employee_TypesRepository.CreateEmployee_Types(Employee_TypesDetails);
                _logger.LogInformation($"database call done successfully with {employee_typesDTO?.Id}");
                return Ok(employee_typesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateEmployee_Types([FromRoute] int Id, [FromBody] Employee_Types updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Employee_Types employee_Types = await employee_TypesRepository.UpdateEmployee_Types(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {employee_Types}");
                if (employee_Types == null) 
                { 
                    return NotFound(); 
                }
                return Ok(employee_Types);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateEmployee_TypesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateEmployee_TypesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Employee_Types employee_Types = await employee_TypesRepository.UpdateEmployee_TypesStatus(Id);
                _logger.LogInformation($"database call done successfully with {employee_Types}");
                if (employee_Types == null) 
                { 
                    return NotFound(); 
                }
                return Ok(employee_Types);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteEmployee_Types(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await employee_TypesRepository.DeleteEmployee_Types(Id);
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

	  [HttpGet("~/GetEmployee_TypesLookup")]
        public async Task<IActionResult> GetEmployee_TypesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var employee_typesList = await employee_TypesRepository.GetEmployee_TypesLookup();
                _logger.LogInformation($"database call done successfully with {employee_typesList?.Count()}");
                return Ok(employee_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchEmployee_Types")]
        public async Task<IActionResult> SearchEmployee_Types(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
                var employee_typesList = employee_TypesRepository.SearchEmployee_Types(searchText, pageNumber, pageSize, sortColumn, sortOrder);
                _logger.LogInformation($"database call done successfully with {employee_typesList?.Count()}");
                return Ok(employee_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
