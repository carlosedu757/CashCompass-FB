document.querySelector('#btnSaveCategory').addEventListener('click', function () {
    // Obter o valor do campo de texto do modal
    const nomeCategoria = document.getElementById('nameCategoryCreate').value;
    console.log(nomeCategoria);

    const data = { nome: nomeCategoria }
    // Dados a serem enviados para a API
    if (data.nome.trim() === '') {
        alert('Por favor, insira um nome para a categoria.');
        return;
    }

    // Fazer uma solicitação POST para o endpoint da API
    fetch('http://localhost:5134/api/v1/Categoria', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Ocorreu um erro ao enviar os dados.');
        }
        return response.json();
    })
    .then(result => {
        // Lógica após a conclusão da requisição, se necessário
        console.log('Dados enviados com sucesso:', result);

        exibirMensagem('Solicitação enviada com sucesso!', 'success');
    })
    .catch(error => {
        console.error('Erro ao enviar os dados:', error);
        // Tratar o erro ou fornecer feedback ao usuário, se necessário
        exibirMensagem('Erro ao enviar os dados. Tente novamente.', 'error');
    });
});

function exibirMensagem(mensagem, tipo) {
    const feedbackMessage = document.getElementById('feedbackMessage');
    feedbackMessage.textContent = mensagem;

    // Definir classes para estilização da mensagem
    if (tipo === 'success') {
        feedbackMessage.style.color = 'green';
    } else if (tipo === 'error') {
        feedbackMessage.style.color = 'red';
    }

    // Exibir a mensagem
    feedbackMessage.style.display = 'block';

    // Esconder a mensagem após alguns segundos (opcional)
    setTimeout(() => {
        feedbackMessage.style.display = 'none';
    }, 3000); // Exibe a mensagem por 3 segundos (3000 milissegundos)
}

// Função para preencher a tabela com os dados das categorias
function preencherTabelaCategorias(categorias) {
    const tabelaCategorias = document.getElementById('tabelaCategorias');

    categorias.forEach(categoria => {
        // Criar uma nova linha <tr>
        const novaLinha = document.createElement('tr');

        // Criar o primeiro <td> com o nome da categoria
        const tdNome = document.createElement('td');
        tdNome.textContent = categoria.nome; // Supondo que o nome da categoria seja acessado assim

        // Criar o segundo <td> com as opções de edição e exclusão
        const tdOpcoes = document.createElement('td');
        const iconsContainer = document.createElement('div');
        iconsContainer.classList.add('icons-container');

        // Ícone de edição
        const iconEdit = document.createElement('a');
        const iconeWrench = document.createElement('i');
        iconeWrench.classList.add('fas', 'fa-wrench');
        iconeWrench.style.color = 'rgb(0, 0, 0)';
        iconeWrench.dataset.toggle = 'modal';
        iconeWrench.dataset.target = '.bd-editcategory-modal-lg';
        iconEdit.appendChild(iconeWrench);

        // Ícone de exclusão
        const iconDelete = document.createElement('a');
        const iconeTrash = document.createElement('i');
        iconeTrash.classList.add('fas', 'fa-trash');
        iconeTrash.style.color = 'red';
        iconeTrash.dataset.toggle = 'modal';
        iconeTrash.dataset.target = '#apagarCategoriaModal';
        iconDelete.appendChild(iconeTrash);

        // Adicionar ícones ao container
        iconsContainer.appendChild(iconEdit);
        iconsContainer.appendChild(iconDelete);

        // Adicionar o container de ícones ao <td>
        tdOpcoes.appendChild(iconsContainer);

        // Adicionar os <td>s à linha <tr>
        novaLinha.appendChild(tdNome);
        novaLinha.appendChild(tdOpcoes);

        // Adicionar a linha à tabela
        tabelaCategorias.appendChild(novaLinha);
    });
}

// Supondo que você já tenha o código para fazer a requisição à sua API e obtenha os dados
fetch('http://localhost:5134/api/v1/Categoria', { 
    method: 'GET' 
})
.then(response => response.json())
.then(data => {
    preencherTabelaCategorias(data); // Chamar a função para preencher a tabela com os dados obtidos
})
.catch(error => {
    console.error('Erro ao obter dados:', error);
});

document.querySelector('#btnSaveCategory').addEventListener('click', function () {
    // Fecha o modal usando jQuery (caso esteja usando Bootstrap)
    $('.addcategory-modal-lg').modal('hide');
});

document.querySelector('#apagarCategory').addEventListener('click', function () {
    // Fecha o modal usando jQuery (caso esteja usando Bootstrap)
    $('#apagarCategoriaModal').modal('hide');
});
