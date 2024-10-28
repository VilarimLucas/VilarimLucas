using portifolio_lucas_vilarim_api_rest.Data;
using portifolio_lucas_vilarim_api_rest.Models;
using portifolio_lucas_vilarim_api_rest.Repositories.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;

namespace portifolio_lucas_vilarim_api_rest.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly FirebaseClient _firebaseClient;

        public SkillRepository(FirebaseContext context)
        {
            _firebaseClient = context.Client;
        }

        // Método para adicionar uma nova habilidade
        public async Task<SkillModel> AddSkill(SkillModel skill)
        {
            var result = await _firebaseClient
                .Child("Skills")
                .PostAsync(skill);

            skill.Id = result.Key; // Armazena o ID gerado automaticamente pelo Firebase
            return skill;
        }

        // Método para listar todas as habilidades
        public async Task<List<SkillModel>> GetAllSkills()
        {
            var skills = await _firebaseClient
                .Child("Skills")
                .OnceAsync<SkillModel>();

            return skills.Select(s => new SkillModel
            {
                Id = s.Key,
                NameSkill = s.Object.NameSkill,
                DescriptionSkill = s.Object.DescriptionSkill,
                PercentageSkill = s.Object.PercentageSkill
            }).ToList();
        }

        // Método para obter uma habilidade por ID
        public async Task<SkillModel> GetSkillById(string id)
        {
            var skill = await _firebaseClient
                .Child("Skills")
                .Child(id)
                .OnceSingleAsync<SkillModel>();

            if (skill == null)
            {
                return null;
            }

            skill.Id = id; // Certifica que o ID é mantido no retorno
            return skill;
        }

        // Método para atualizar uma habilidade existente
        public async Task<SkillModel> UpdateSkill(SkillModel skill, string id)
        {
            var existingSkill = await GetSkillById(id);

            if (existingSkill == null)
            {
                throw new KeyNotFoundException($"Habilidade com o ID: {id} não foi encontrada no Firebase.");
            }

            skill.Id = id; // Certifica que o ID seja o mesmo na atualização
            await _firebaseClient
                .Child("Skills")
                .Child(id)
                .PutAsync(skill);

            return skill;
        }

        // Método para deletar uma habilidade
        public async Task<bool> DeleteSkill(string id)
        {
            var existingSkill = await GetSkillById(id);

            if (existingSkill == null)
            {
                throw new KeyNotFoundException($"Habilidade com o ID: {id} não foi encontrada no Firebase.");
            }

            await _firebaseClient
                .Child("Skills")
                .Child(id)
                .DeleteAsync();

            return true;
        }
    }
}
