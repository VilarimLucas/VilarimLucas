﻿using Microsoft.AspNetCore.Mvc;
using portifolio_lucas_vilarim_api_rest.Models;
using portifolio_lucas_vilarim_api_rest.Repositories.Interfaces;

namespace portifolio_lucas_vilarim_api_rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;

        public SkillController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        // Método para listar todas as habilidades
        [HttpGet]
        public async Task<ActionResult<List<SkillModel>>> GetSkills()
        {
            var skills = await _skillRepository.GetAllSkills();
            return Ok(skills);
        }

        // Método para obter uma habilidade específica por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillModel>> GetSkill(string id)
        {
            var skill = await _skillRepository.GetSkillById(id);

            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        // Método para criar uma nova habilidade
        [HttpPost]
        public async Task<ActionResult<SkillModel>> CreateSkill(SkillModel skill)
        {
            var createdSkill = await _skillRepository.AddSkill(skill);
            return CreatedAtAction(nameof(GetSkill), new { id = createdSkill.Id }, createdSkill);
        }

        // Método para atualizar uma habilidade existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(string id, SkillModel skill)
        {
            var existingSkill = await _skillRepository.GetSkillById(id);

            if (existingSkill == null)
            {
                return NotFound();
            }

            skill.Id = id;
            await _skillRepository.UpdateSkill(skill, id);

            return NoContent();
        }

        // Método para deletar uma habilidade por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(string id)
        {
            var existingSkill = await _skillRepository.GetSkillById(id);

            if (existingSkill == null)
            {
                return NotFound();
            }

            await _skillRepository.DeleteSkill(id);

            return NoContent();
        }
    }
}
