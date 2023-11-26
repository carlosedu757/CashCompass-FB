obterDadosETabela();

document.querySelector('#btnSaveCategory').addEventListener('click', function () {
    // Obter o valor do campo de texto do modal
    const nomeCategoria = document.getElementById('nameCategoryCreate').value;

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
        $('.addcategory-modal-lg').modal('hide');
        obterDadosETabela();
        document.getElementById('nameCategoryCreate').value = '';
        // exibirMensagem('Solicitação enviada com sucesso!', 'success');
    })
    .catch(error => {
        console.error('Erro ao enviar os dados:', error);
        // Tratar o erro ou fornecer feedback ao usuário, se necessário
        // exibirMensagem('Erro ao enviar os dados. Tente novamente.', 'error');
    });
});

// function exibirMensagem(mensagem, tipo) {
//     const feedbackMessage = document.getElementById('feedbackMessage');
//     feedbackMessage.textContent = mensagem;

//     // Definir classes para estilização da mensagem
//     if (tipo === 'success') {
//         feedbackMessage.style.color = 'green';
//     } else if (tipo === 'error') {
//         feedbackMessage.style.color = 'red';
//     }

//     // Exibir a mensagem
//     feedbackMessage.style.display = 'block';

//     // Esconder a mensagem após alguns segundos (opcional)
//     setTimeout(() => {
//         feedbackMessage.style.display = 'none';
//     }, 3000); // Exibe a mensagem por 3 segundos (3000 milissegundos)
// }

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
        iconeWrench.dataset.id = categoria.categoriaId;
        iconEdit.appendChild(iconeWrench);

        // Ícone de exclusão
        const iconDelete = document.createElement('a');
        const iconeTrash = document.createElement('i');
        iconeTrash.classList.add('fas', 'fa-trash');
        iconeTrash.style.color = 'red';
        iconeTrash.dataset.toggle = 'modal';
        iconeTrash.dataset.target = '#apagarCategoriaModal';
        iconeTrash.dataset.id = categoria.categoriaId;
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

    $('#dataTableCategory').DataTable();
}

// Supondo que você já tenha o código para fazer a requisição à sua API e obtenha os dados
function obterDadosETabela() {
    // Limpar a tabela antes de preencher com novos dados
    const tabelaCategorias = document.getElementById('tabelaCategorias');
    tabelaCategorias.innerHTML = '';

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
}

$('#apagarCategoriaModal').on('show.bs.modal', function (event) {
    const button = $(event.relatedTarget); // Botão que acionou o modal
    const categoriaId = button.data('id'); // Capturando o ID da categoria do botão que acionou o modal

    // Capturando o botão "Apagar" dentro do modal
    const apagarButton = $(this).find('#apagarCategory');

    // Removendo qualquer evento de clique previamente atribuído
    apagarButton.off('click');

    // Evento de clique no botão "Apagar" dentro do modal
    apagarButton.on('click', function () {
        console.log(categoriaId); // Verificando se o ID está sendo capturado corretamente

        // Fazer uma solicitação DELETE para o endpoint da API para excluir a categoria
        fetch('http://localhost:5134/api/v1/Categoria/' + categoriaId, {
            method: 'DELETE'
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao excluir categoria.');
            }
            $('#apagarCategoriaModal').modal('hide');
            obterDadosETabela();

            return response.json;
        })
        .then(data => {
            // Exibir o ID da categoria excluída
            $('#categoriaExcluida').text(`${data.nome}`); // Substitua 'data.id' pela propriedade correta

            // Exibir a div de alerta de sucesso após a exclusão
            $('#apagarCategoriaSuccess').show(); // Supondo que a div tenha um ID de alertSuccess

            // Esconder a div de alerta após 5 segundos
            setTimeout(() => {
                $('#apagarCategoriaSuccess').hide();
            }, 5000);
        })
        .catch(error => {
            console.error('Erro ao excluir categoria:', error);
            alert('Erro ao excluir categoria. Tente novamente.');
        });
    });
});

function pesquisarCategoria() {
    const nomeCategoria = document.getElementById('nameCategorySearch').value;
    if(nomeCategoria === "")
    {
        obterDadosETabela();
    }
    else
    {
        fetch(`http://localhost:5134/api/v1/Categoria/search?nome=${nomeCategoria}`, {
            method: 'GET'
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao obter os dados.');
            }
            return response.json();
        })
        .then(data => {
            const tabelaCategorias = document.getElementById('tabelaCategorias');
            tabelaCategorias.innerHTML = '';
            preencherTabelaCategorias(data);
        })
        .catch(error => {
            console.error('Erro ao obter dados:', error);
        });
    }
}

const botaoPesquisar = document.getElementById('buttonSearchCategory');
botaoPesquisar.addEventListener('click', function(event) {
    event.preventDefault();
    pesquisarCategoria();
});


$('.bd-editcategory-modal-lg').on('show.bs.modal', function (event) {
    const button = $(event.relatedTarget); // Botão que acionou o modal
    const categoriaId = button.data('id'); // Capturando o ID da categoria do botão que acionou o modal

    // Capturando o botão "Apagar" dentro do modal
    const editarButton = $(this).find('#btnEditCategory');

    // Removendo qualquer evento de clique previamente atribuído
    editarButton.off('click');

    // Evento de clique no botão "Apagar" dentro do modal
    editarButton.on('click', function () {
        const nome = document.getElementById('nameCategoryEdit').value;

        const data = { CategoriaId: categoriaId, Nome: nome }; // Verificando se o ID está sendo capturado corretamente

        // Fazer uma solicitação DELETE para o endpoint da API para excluir a categoria
        fetch(`http://localhost:5134/api/v1/Categoria/${categoriaId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Ocorreu um erro ao editar a categoria.');
            }
            $('.bd-editcategory-modal-lg').modal('hide');
            obterDadosETabela();

            return response.json();
        })
        .then(result => {
            console.log('Categoria editada com sucesso:', result);
            document.getElementById('nameCategoryEdit').value = '';
            // Lógica após a edição da categoria, se necessário
        })
        .catch(error => {
            console.error('Erro ao editar a categoria:', error);
            // Tratar o erro ou fornecer feedback ao usuário, se necessário
        });
    });
});