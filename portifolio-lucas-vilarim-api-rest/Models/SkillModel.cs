namespace portifolio_lucas_vilarim_api_rest.Models
{
    public class SkillModel
    {
        // Propriedades encapsuladas
        // Nome da habilidade
        public string? Id { get; set; } // Id do projeto, gerado automaticamente pelo Firebase

        public string NameSkill { get; set; }
        // Descrição da habilidade
        public string DescriptionSkill { get; set; }
        // Porcentagem da habilidade
        public int PercentageSkill { get; set; }

        // Construtor padrão
        public SkillModel() { }

        // Construtor com parâmetros
        public SkillModel(string nameSkill, string descriptionSkill, int percentageSkill)
        {
            NameSkill = nameSkill;
            DescriptionSkill = descriptionSkill;
            PercentageSkill = percentageSkill;
        }
    }
}
