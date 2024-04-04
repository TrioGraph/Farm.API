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
    public class Document_TypeController : Controller
    {
        private readonly IDocument_TypeRepository document_TypeRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Document_TypeController> _logger;
        public Document_TypeController(IDocument_TypeRepository document_TypeRepository, IMapper mapper, ILogger<Document_TypeController> logger)
        {
            this.document_TypeRepository = document_TypeRepository;
            mapper = mapper;
            _logger = logger;
        }

        [HttpGet("~/GetAllDocument_Type")]
        public async Task<IActionResult> GetAllDocument_Type()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var document_typeList = await document_TypeRepository.GetAllDocument_Type();
                _logger.LogInformation($"database call done successfully with {document_typeList?.Count()}");
                return Ok(document_typeList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetDocument_TypeById")]
        public async Task<IActionResult> GetDocument_TypeById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var document_typeList = await document_TypeRepository.GetDocument_TypeById(Id);
                _logger.LogInformation($"database call done successfully with {document_typeList?.Id}");
                if (document_typeList == null)
                {
                    return NotFound();
                }
                return Ok(document_typeList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddDocument_Type")]
        public async Task<IActionResult> CreateDocument_Type(Document_Type Document_TypeDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var document_Type = new Document_Type()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var document_typeDTO = await document_TypeRepository.CreateDocument_Type(Document_TypeDetails);
                _logger.LogInformation($"database call done successfully with {document_typeDTO?.Id}");
                return Ok(document_typeDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateDocument_Type([FromRoute] int Id, [FromBody] Document_Type updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Document_Type document_Type = await document_TypeRepository.UpdateDocument_Type(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {document_Type}");
                if (document_Type == null) 
                { 
                    return NotFound(); 
                }
                return Ok(document_Type);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateDocument_TypeStatus/{Id:int}")]
        public async Task<IActionResult> UpdateDocument_TypeStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Document_Type document_Type = await document_TypeRepository.UpdateDocument_TypeStatus(Id);
                _logger.LogInformation($"database call done successfully with {document_Type}");
                if (document_Type == null) 
                { 
                    return NotFound(); 
                }
                return Ok(document_Type);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteDocument_Type(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await document_TypeRepository.DeleteDocument_Type(Id);
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

	  [HttpGet("~/GetDocument_TypeLookup")]
        public async Task<IActionResult> GetDocument_TypeLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var document_typeList = await document_TypeRepository.GetDocument_TypeLookup();
                _logger.LogInformation($"database call done successfully with {document_typeList?.Count()}");
                return Ok(document_typeList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchDocument_Type")]
        public async Task<IActionResult> SearchDocument_Type(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
                var document_typeList = document_TypeRepository.SearchDocument_Type(searchText, pageNumber, pageSize, sortColumn, sortOrder);
                _logger.LogInformation($"database call done successfully with {document_typeList?.Count()}");
                return Ok(document_typeList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
