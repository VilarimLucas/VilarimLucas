using portifolio_lucas_vilarim_api_rest.Data;
using portifolio_lucas_vilarim_api_rest.Models;
using portifolio_lucas_vilarim_api_rest.Repositories.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portifolio_lucas_vilarim_api_rest.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly FirebaseClient _firebaseClient;

        public ProjectRepository(FirebaseContext context)
        {
            _firebaseClient = context.Client;
        }

        // Método para adicionar um novo projeto
        public async Task<ProjectModel> AddProject(ProjectModel project)
        {
            var result = await _firebaseClient
                .Child("Projects")
                .PostAsync(project);

            project.Id = result.Key; // Armazena o ID gerado automaticamente pelo Firebase
            return project;
        }

        // Método para listar todos os projetos
        public async Task<List<ProjectModel>> GetAllProjects()
        {
            var projects = await _firebaseClient
                .Child("Projects")
                .OnceAsync<ProjectModel>();

            return projects.Select(p => new ProjectModel
            {
                Id = p.Key,
                ClientProject = p.Object.ClientProject,
                DataProject = p.Object.DataProject,
                UrlProject = p.Object.UrlProject,
                NameProject = p.Object.NameProject,
                ContentProject = p.Object.ContentProject,
                ResumeProject = p.Object.ResumeProject
            }).ToList();
        }

        // Método para obter um projeto por ID
        public async Task<ProjectModel> GetProjectById(string id)
        {
            var project = await _firebaseClient
                .Child("Projects")
                .Child(id)
                .OnceSingleAsync<ProjectModel>();

            if (project == null)
            {
                return null;
            }

            project.Id = id; // Certifica que o ID é mantido no retorno
            return project;
        }

        // Método para atualizar um projeto existente
        public async Task<ProjectModel> UpdateProject(ProjectModel project, string id)
        {
            var existingProject = await GetProjectById(id);

            if (existingProject == null)
            {
                throw new KeyNotFoundException($"Projeto com o ID: {id} não foi encontrado no Firebase.");
            }

            project.Id = id; // Certifica que o ID seja o mesmo na atualização
            await _firebaseClient
                .Child("Projects")
                .Child(id)
                .PutAsync(project);

            return project;
        }

        // Método para deletar um projeto
        public async Task<bool> DeleteProject(string id)
        {
            var existingProject = await GetProjectById(id);

            if (existingProject == null)
            {
                throw new KeyNotFoundException($"Projeto com o ID: {id} não foi encontrado no Firebase.");
            }

            await _firebaseClient
                .Child("Projects")
                .Child(id)
                .DeleteAsync();

            return true;
        }
    }
}
