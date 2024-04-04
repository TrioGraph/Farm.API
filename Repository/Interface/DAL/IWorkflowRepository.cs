using Farm.Models;
using Farm.Models.Lookup;

namespace Farm.Repositories
{
    public interface IWorkflowRepository
    {
        Task<List<Workflow>> GetAllWorkflow();
        
        Task<IEnumerable<object>> GetWorkflowLookup();
		
        Task<Workflow> GetWorkflowById(int id);

        Task<Workflow> CreateWorkflow(Workflow appsPermissions);

        Task<Workflow> UpdateWorkflow(int Id, Workflow role);
    
        Task<Workflow> UpdateWorkflowStatus(int Id);

        Task<IEnumerable<Workflow>> DeleteWorkflow(int roles);
       
        Dictionary<string, object> SearchWorkflow(string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection);

    }
}