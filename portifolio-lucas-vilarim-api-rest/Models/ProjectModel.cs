namespace portifolio_lucas_vilarim_api_rest.Models
{
    public class ProjectModel
    {
        public string Id { get; set; } // Id do projeto, gerado automaticamente pelo Firebase
        public string ClientProject { get; set; } // Nome do cliente do projeto
        public DateTime DataProject { get; set; } // Data do projeto
        public string UrlProject { get; set; } // URL do projeto
        public string NameProject { get; set; } // Nome do projeto
        public string ContentProject { get; set; } // Conteúdo ou descrição detalhada do projeto
        public string ResumeProject { get; set; } // Resumo do projeto

        // Construtor padrão
        public ProjectModel() { }

        // Construtor com parâmetros
        public ProjectModel(string clientProject, DateTime dataProject, string urlProject, string nameProject, string contentProject, string resumeProject)
        {
            ClientProject = clientProject;
            DataProject = dataProject;
            UrlProject = urlProject;
            NameProject = nameProject;
            ContentProject = contentProject;
            ResumeProject = resumeProject;
        }
    }
}
