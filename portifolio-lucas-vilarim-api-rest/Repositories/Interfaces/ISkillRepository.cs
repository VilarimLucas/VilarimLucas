using portifolio_lucas_vilarim_api_rest.Models;

namespace portifolio_lucas_vilarim_api_rest.Repositories.Interfaces
{
    public interface ISkillRepository
    {
        Task<List<SkillModel>> GetAllSkills();
        Task<SkillModel> GetSkillById(string id);
        Task<SkillModel> AddSkill(SkillModel skill);
        Task<SkillModel> UpdateSkill(SkillModel skill, string id);
        Task<bool> DeleteSkill(string id);
    }
}
