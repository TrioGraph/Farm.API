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
    public class Farmers_LoginController : Controller
    {
        private readonly IFarmers_LoginRepository farmers_LoginRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Farmers_LoginController> _logger;
        public Farmers_LoginController(IFarmers_LoginRepository farmers_LoginRepository, IMapper mapper, ILogger<Farmers_LoginController> logger)
        {
            this.farmers_LoginRepository = farmers_LoginRepository;
            mapper = mapper;
            _logger = logger;
        }

        [HttpGet("~/GetAllFarmers_Login")]
        public async Task<IActionResult> GetAllFarmers_Login()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var farmers_loginList = await farmers_LoginRepository.GetAllFarmers_Login();
                _logger.LogInformation($"database call done successfully with {farmers_loginList?.Count()}");
                return Ok(farmers_loginList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetFarmers_LoginById")]
        public async Task<IActionResult> GetFarmers_LoginById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var farmers_loginList = await farmers_LoginRepository.GetFarmers_LoginById(Id);
                _logger.LogInformation($"database call done successfully with {farmers_loginList?.Id}");
                if (farmers_loginList == null)
                {
                    return NotFound();
                }
                return Ok(farmers_loginList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddFarmers_Login")]
        public async Task<IActionResult> CreateFarmers_Login(Farmers_Login Farmers_LoginDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var farmers_Login = new Farmers_Login()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var farmers_loginDTO = await farmers_LoginRepository.CreateFarmers_Login(Farmers_LoginDetails);
                _logger.LogInformation($"database call done successfully with {farmers_loginDTO?.Id}");
                return Ok(farmers_loginDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateFarmers_Login([FromRoute] int Id, [FromBody] Farmers_Login updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farmers_Login farmers_Login = await farmers_LoginRepository.UpdateFarmers_Login(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {farmers_Login}");
                if (farmers_Login == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmers_Login);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateFarmers_LoginStatus/{Id:int}")]
        public async Task<IActionResult> UpdateFarmers_LoginStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farmers_Login farmers_Login = await farmers_LoginRepository.UpdateFarmers_LoginStatus(Id);
                _logger.LogInformation($"database call done successfully with {farmers_Login}");
                if (farmers_Login == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmers_Login);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteFarmers_Login(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await farmers_LoginRepository.DeleteFarmers_Login(Id);
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

	  [HttpGet("~/GetFarmers_LoginLookup")]
        public async Task<IActionResult> GetFarmers_LoginLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var farmers_loginList = await farmers_LoginRepository.GetFarmers_LoginLookup();
                _logger.LogInformation($"database call done successfully with {farmers_loginList?.Count()}");
                return Ok(farmers_loginList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchFarmers_Login")]
        public async Task<IActionResult> SearchFarmers_Login(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
                var farmers_loginList = farmers_LoginRepository.SearchFarmers_Login(searchText, pageNumber, pageSize, sortColumn, sortOrder);
                _logger.LogInformation($"database call done successfully with {farmers_loginList?.Count()}");
                return Ok(farmers_loginList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
