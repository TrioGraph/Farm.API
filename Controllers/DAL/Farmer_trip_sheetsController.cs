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
    public class Farmer_trip_sheetsController : Controller
    {
        private readonly IFarmer_trip_sheetsRepository farmer_trip_sheetsRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Farmer_trip_sheetsController> _logger;
	private IUtilityHelper utilityHelper;
        public Farmer_trip_sheetsController(IFarmer_trip_sheetsRepository farmer_trip_sheetsRepository, IMapper mapper, ILogger<Farmer_trip_sheetsController> logger,
	IUtilityHelper utilityHelper)
        {
            this.farmer_trip_sheetsRepository = farmer_trip_sheetsRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllFarmer_trip_sheets")]
        public async Task<IActionResult> GetAllFarmer_trip_sheets()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var farmer_trip_sheetsList = await farmer_trip_sheetsRepository.GetAllFarmer_trip_sheets();
                _logger.LogInformation($"database call done successfully with {farmer_trip_sheetsList?.Count()}");
                return Ok(farmer_trip_sheetsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetFarmer_trip_sheetsById")]
        public async Task<IActionResult> GetFarmer_trip_sheetsById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var farmer_trip_sheetsList = await farmer_trip_sheetsRepository.GetFarmer_trip_sheetsById(Id);
                _logger.LogInformation($"database call done successfully with {farmer_trip_sheetsList?.Id}");
                if (farmer_trip_sheetsList == null)
                {
                    return NotFound();
                }
                return Ok(farmer_trip_sheetsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddFarmer_trip_sheets")]
        public async Task<IActionResult> CreateFarmer_trip_sheets(Farmer_trip_sheets Farmer_trip_sheetsDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var farmer_trip_sheets = new Farmer_trip_sheets()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var farmer_trip_sheetsDTO = await farmer_trip_sheetsRepository.CreateFarmer_trip_sheets(Farmer_trip_sheetsDetails);
                _logger.LogInformation($"database call done successfully with {farmer_trip_sheetsDTO?.Id}");
                return Ok(farmer_trip_sheetsDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateFarmer_trip_sheets([FromRoute] int Id, [FromBody] Farmer_trip_sheets updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farmer_trip_sheets farmer_trip_sheets = await farmer_trip_sheetsRepository.UpdateFarmer_trip_sheets(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {farmer_trip_sheets}");
                if (farmer_trip_sheets == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmer_trip_sheets);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateFarmer_trip_sheetsStatus/{Id:int}")]
        public async Task<IActionResult> UpdateFarmer_trip_sheetsStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Farmer_trip_sheets farmer_trip_sheets = await farmer_trip_sheetsRepository.UpdateFarmer_trip_sheetsStatus(Id);
                _logger.LogInformation($"database call done successfully with {farmer_trip_sheets}");
                if (farmer_trip_sheets == null) 
                { 
                    return NotFound(); 
                }
                return Ok(farmer_trip_sheets);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteFarmer_trip_sheets(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await farmer_trip_sheetsRepository.DeleteFarmer_trip_sheets(Id);
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

	  [HttpGet("~/GetFarmer_trip_sheetsLookup")]
        public async Task<IActionResult> GetFarmer_trip_sheetsLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var farmer_trip_sheetsList = await farmer_trip_sheetsRepository.GetFarmer_trip_sheetsLookup();
                _logger.LogInformation($"database call done successfully with {farmer_trip_sheetsList?.Count()}");
                return Ok(farmer_trip_sheetsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchFarmer_trip_sheets")]
        public async Task<IActionResult> SearchFarmer_trip_sheets(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var farmer_trip_sheetsList = farmer_trip_sheetsRepository.SearchFarmer_trip_sheets(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {farmer_trip_sheetsList?.Count()}");
                return Ok(farmer_trip_sheetsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
