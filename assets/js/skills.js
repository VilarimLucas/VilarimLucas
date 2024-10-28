document.addEventListener('DOMContentLoaded', function () {
    // Função para carregar as habilidades
    async function loadSkills() {
        try {
            const response = await axios.get('https://localhost:44383/api/Skill');
            const skills = response.data;

            // Seleciona o container das habilidades
            const skillsContainer = document.querySelector('.skills-progress');
            // Limpa o conteúdo existente
            skillsContainer.innerHTML = '';
            // Itera sobre as habilidades e cria os elementos HTML
            skills.forEach(skill => {
                const skillElement = document.createElement('div');
                skillElement.classList.add('progress');

                skillElement.innerHTML = `
                    <span class="skill">${skill.nameSkill} <i class="val">${skill.percentageSkill}%</i></span>
                    <div class="progress-bar-wrap">
                        <div class="progress-bar" role="progressbar" aria-valuenow=${skill.percentageSkill} aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                `;
                skillsContainer.appendChild(skillElement);
            });
        } catch (error) {
            console.error('Erro ao carregar as habilidades:', error);
        }
    }
    // Chama a função para carregar as habilidades
    loadSkills();
});
