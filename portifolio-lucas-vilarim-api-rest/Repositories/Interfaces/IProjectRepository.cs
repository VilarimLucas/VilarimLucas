using portifolio_lucas_vilarim_api_rest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portifolio_lucas_vilarim_api_rest.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<ProjectModel>> GetAllProjects();
        Task<ProjectModel> GetProjectById(string id);
        Task<ProjectModel> AddProject(ProjectModel project);
        Task<ProjectModel> UpdateProject(ProjectModel project, string id);
        Task<bool> DeleteProject(string id);
    }
}
