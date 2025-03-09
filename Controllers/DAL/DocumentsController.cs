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
    public class DocumentsController : Controller
    {
        private readonly IDocumentsRepository documentsRepository;
        private readonly IMapper mapper;
        private readonly ILogger<DocumentsController> _logger;
	private IUtilityHelper utilityHelper;
        public DocumentsController(IDocumentsRepository documentsRepository, IMapper mapper, ILogger<DocumentsController> logger,
	IUtilityHelper utilityHelper)
        {
            this.documentsRepository = documentsRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllDocuments")]
        public async Task<IActionResult> GetAllDocuments()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var documentsList = await documentsRepository.GetAllDocuments();
                _logger.LogInformation($"database call done successfully with {documentsList?.Count()}");
                return Ok(documentsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetDocumentsById")]
        public async Task<IActionResult> GetDocumentsById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var documentsList = await documentsRepository.GetDocumentsById(Id);
                _logger.LogInformation($"database call done successfully with {documentsList?.Id}");
                if (documentsList == null)
                {
                    return NotFound();
                }
                return Ok(documentsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddDocuments")]
        public async Task<IActionResult> CreateDocuments(Documents DocumentsDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var documents = new Documents()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var documentsDTO = await documentsRepository.CreateDocuments(DocumentsDetails);
                _logger.LogInformation($"database call done successfully with {documentsDTO?.Id}");
                return Ok(documentsDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateDocuments([FromRoute] int Id, [FromBody] Documents updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Documents documents = await documentsRepository.UpdateDocuments(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {documents}");
                if (documents == null) 
                { 
                    return NotFound(); 
                }
                return Ok(documents);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateDocumentsStatus/{Id:int}")]
        public async Task<IActionResult> UpdateDocumentsStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Documents documents = await documentsRepository.UpdateDocumentsStatus(Id);
                _logger.LogInformation($"database call done successfully with {documents}");
                if (documents == null) 
                { 
                    return NotFound(); 
                }
                return Ok(documents);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteDocuments(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await documentsRepository.DeleteDocuments(Id);
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

	  [HttpGet("~/GetDocumentsLookup")]
        public async Task<IActionResult> GetDocumentsLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var documentsList = await documentsRepository.GetDocumentsLookup();
                _logger.LogInformation($"database call done successfully with {documentsList?.Count()}");
                return Ok(documentsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchDocuments")]
        public async Task<IActionResult> SearchDocuments(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var documentsList = documentsRepository.SearchDocuments(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {documentsList?.Count()}");
                return Ok(documentsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
