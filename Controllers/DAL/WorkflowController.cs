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
    public class WorkflowController : Controller
    {
        private readonly IWorkflowRepository workflowRepository;
        private readonly IMapper mapper;
        private readonly ILogger<WorkflowController> _logger;
	private IUtilityHelper utilityHelper;
        public WorkflowController(IWorkflowRepository workflowRepository, IMapper mapper, ILogger<WorkflowController> logger,
	IUtilityHelper utilityHelper)
        {
            this.workflowRepository = workflowRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllWorkflow")]
        public async Task<IActionResult> GetAllWorkflow()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var workflowList = await workflowRepository.GetAllWorkflow();
                _logger.LogInformation($"database call done successfully with {workflowList?.Count()}");
                return Ok(workflowList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetWorkflowById")]
        public async Task<IActionResult> GetWorkflowById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var workflowList = await workflowRepository.GetWorkflowById(Id);
                _logger.LogInformation($"database call done successfully with {workflowList?.Id}");
                if (workflowList == null)
                {
                    return NotFound();
                }
                return Ok(workflowList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddWorkflow")]
        public async Task<IActionResult> CreateWorkflow(Workflow WorkflowDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var workflow = new Workflow()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var workflowDTO = await workflowRepository.CreateWorkflow(WorkflowDetails);
                _logger.LogInformation($"database call done successfully with {workflowDTO?.Id}");
                return Ok(workflowDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateWorkflow([FromRoute] int Id, [FromBody] Workflow updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Workflow workflow = await workflowRepository.UpdateWorkflow(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {workflow}");
                if (workflow == null) 
                { 
                    return NotFound(); 
                }
                return Ok(workflow);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateWorkflowStatus/{Id:int}")]
        public async Task<IActionResult> UpdateWorkflowStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Workflow workflow = await workflowRepository.UpdateWorkflowStatus(Id);
                _logger.LogInformation($"database call done successfully with {workflow}");
                if (workflow == null) 
                { 
                    return NotFound(); 
                }
                return Ok(workflow);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteWorkflow(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await workflowRepository.DeleteWorkflow(Id);
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

	  [HttpGet("~/GetWorkflowLookup")]
        public async Task<IActionResult> GetWorkflowLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var workflowList = await workflowRepository.GetWorkflowLookup();
                _logger.LogInformation($"database call done successfully with {workflowList?.Count()}");
                return Ok(workflowList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchWorkflow")]
        public async Task<IActionResult> SearchWorkflow(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var workflowList = workflowRepository.SearchWorkflow(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {workflowList?.Count()}");
                return Ok(workflowList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
