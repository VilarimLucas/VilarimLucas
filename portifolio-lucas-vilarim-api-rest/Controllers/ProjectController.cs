using Microsoft.AspNetCore.Mvc;
using portifolio_lucas_vilarim_api_rest.Models;
using portifolio_lucas_vilarim_api_rest.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portifolio_lucas_vilarim_api_rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // Método para listar todos os projetos
        [HttpGet]
        public async Task<ActionResult<List<ProjectModel>>> GetProjects()
        {
            var projects = await _projectRepository.GetAllProjects();
            return Ok(projects);
        }

        // Método para obter um projeto específico por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectModel>> GetProject(string id)
        {
            var project = await _projectRepository.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // Método para criar um novo projeto
        [HttpPost]
        public async Task<ActionResult<ProjectModel>> CreateProject(ProjectModel project)
        {
            var createdProject = await _projectRepository.AddProject(project);
            return CreatedAtAction(nameof(GetProject), new { id = createdProject.Id }, createdProject);
        }

        // Método para atualizar um projeto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(string id, ProjectModel project)
        {
            var existingProject = await _projectRepository.GetProjectById(id);

            if (existingProject == null)
            {
                return NotFound();
            }

            project.Id = id;
            await _projectRepository.UpdateProject(project, id);

            return NoContent();
        }

        // Método para deletar um projeto por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            var existingProject = await _projectRepository.GetProjectById(id);

            if (existingProject == null)
            {
                return NotFound();
            }

            await _projectRepository.DeleteProject(id);

            return NoContent();
        }
    }
}
