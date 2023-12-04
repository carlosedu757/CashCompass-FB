function obterNomeUsuario() {
    fetch('http://localhost:5134/api/v1/User/userinfo', {
        method: 'GET'
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao obter nome do usuário');
        }
        return response.json();
    })
    .then(data => {
        const nomeUsuario = data.UserName;
        atualizarNomeUsuario(nomeUsuario);
    })
    .catch(error => {
        console.error('Erro ao obter nome do usuário:', error);
    });
}

function atualizarNomeUsuario(nomeUsuario) {
    const elementoNomeUsuario = document.getElementById('nomeUsuario');
    if (elementoNomeUsuario) {
        elementoNomeUsuario.textContent = nomeUsuario;
    }
}

// Chamar a função ao carregar a página
document.addEventListener('DOMContentLoaded', () => {
    obterNomeUsuario();
});
