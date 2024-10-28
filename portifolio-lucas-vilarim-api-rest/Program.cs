using portifolio_lucas_vilarim_api_rest.Data;
using portifolio_lucas_vilarim_api_rest.Repositories.Interfaces;
using portifolio_lucas_vilarim_api_rest.Repositories;

namespace portifolio_lucas_vilarim_api_rest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuração do CORS para permitir requisições do frontend na porta 5500
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5501")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Adicionar configuração do Firebase
            builder.Services.AddSingleton<FirebaseContext>(provider =>
                new FirebaseContext("https://portifolio-lucas-vilarim-default-rtdb.firebaseio.com/"));

            // Injeção de dependência do repositório
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();

            // Adicionar serviços ao contêiner.
            builder.Services.AddControllers();

            // Saiba mais sobre a configuração do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configuração do pipeline de requisição HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Usar a configuração de CORS criada acima
            app.UseCors("AllowFrontend");

            app.MapControllers();

            app.Run();
        }
    }
}
