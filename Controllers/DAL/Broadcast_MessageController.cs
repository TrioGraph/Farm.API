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
    public class Broadcast_MessageController : Controller
    {
        private readonly IBroadcast_MessageRepository broadcast_MessageRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Broadcast_MessageController> _logger;
	private IUtilityHelper utilityHelper;
        public Broadcast_MessageController(IBroadcast_MessageRepository broadcast_MessageRepository, IMapper mapper, ILogger<Broadcast_MessageController> logger,
	IUtilityHelper utilityHelper)
        {
            this.broadcast_MessageRepository = broadcast_MessageRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllBroadcast_Message")]
        public async Task<IActionResult> GetAllBroadcast_Message()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var broadcast_messageList = await broadcast_MessageRepository.GetAllBroadcast_Message();
                _logger.LogInformation($"database call done successfully with {broadcast_messageList?.Count()}");
                return Ok(broadcast_messageList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetBroadcast_MessageById")]
        public async Task<IActionResult> GetBroadcast_MessageById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var broadcast_messageList = await broadcast_MessageRepository.GetBroadcast_MessageById(Id);
                _logger.LogInformation($"database call done successfully with {broadcast_messageList?.Id}");
                if (broadcast_messageList == null)
                {
                    return NotFound();
                }
                return Ok(broadcast_messageList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddBroadcast_Message")]
        public async Task<IActionResult> CreateBroadcast_Message(Broadcast_Message Broadcast_MessageDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var broadcast_Message = new Broadcast_Message()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var broadcast_messageDTO = await broadcast_MessageRepository.CreateBroadcast_Message(Broadcast_MessageDetails);
                _logger.LogInformation($"database call done successfully with {broadcast_messageDTO?.Id}");
                return Ok(broadcast_messageDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateBroadcast_Message([FromRoute] int Id, [FromBody] Broadcast_Message updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Broadcast_Message broadcast_Message = await broadcast_MessageRepository.UpdateBroadcast_Message(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {broadcast_Message}");
                if (broadcast_Message == null) 
                { 
                    return NotFound(); 
                }
                return Ok(broadcast_Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateBroadcast_MessageStatus/{Id:int}")]
        public async Task<IActionResult> UpdateBroadcast_MessageStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Broadcast_Message broadcast_Message = await broadcast_MessageRepository.UpdateBroadcast_MessageStatus(Id);
                _logger.LogInformation($"database call done successfully with {broadcast_Message}");
                if (broadcast_Message == null) 
                { 
                    return NotFound(); 
                }
                return Ok(broadcast_Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteBroadcast_Message(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await broadcast_MessageRepository.DeleteBroadcast_Message(Id);
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

	  [HttpGet("~/GetBroadcast_MessageLookup")]
        public async Task<IActionResult> GetBroadcast_MessageLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var broadcast_messageList = await broadcast_MessageRepository.GetBroadcast_MessageLookup();
                _logger.LogInformation($"database call done successfully with {broadcast_messageList?.Count()}");
                return Ok(broadcast_messageList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchBroadcast_Message")]
        public async Task<IActionResult> SearchBroadcast_Message(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var broadcast_messageList = broadcast_MessageRepository.SearchBroadcast_Message(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {broadcast_messageList?.Count()}");
                return Ok(broadcast_messageList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
