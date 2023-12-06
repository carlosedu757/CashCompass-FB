obterDadosETabela();

const enumTipo = {
    0: 'Cartão de Crédito',
    1: 'Cartão de Débito'
};

const enumBandeira = {
    0: 'Visa',
    1: 'Mastercard',
    2: 'Elo',
    3: 'AmericanExpress (AMEX)',
    4: 'Diners Club International',
    5: 'Hipercard',
    6: 'Aura',
    7: 'Hiper',
    8: 'Cabal',
    9: 'Sorocred',
}

const enumFormaPgt = {
    0: 'Dinheiro',
    1: 'Pix',
    2: 'Transferência Bancária',
    3: 'Cartão de Crédito',
    4: 'Cartão de Débito'
}

fetch('http://localhost:5134/formapagt')
    .then(response => response.json())
    .then(data => {
        popularDropdown(data, 'formaPagamentoRevenueSearchDropdown', 'dropdownFormaPagamentoRevenueSearch');
        popularDropdown(data, 'formaPagamentoRevenueAddDropdown', 'dropdownFormaPagamentoRevenueAdd');
        popularDropdown(data, 'formaPagamentoRevenueEditDropdown', 'dropdownFormaPagamentoRevenueEdit');
    })
    .catch(error => console.error('Erro:', error));

fetch('http://localhost:5134/api/v1/Categoria', {
    method: 'GET'
})
.then(response => response.json())
.then(data => {
    popularDropdownCategoria(data, 'categorySearchRevenueDropdown', 'dropdownCategoryRevenueSearch');
    popularDropdownCategoria(data, 'categoryAddRevenueDropdown', 'dropdownCategoryRevenueAdd');
    popularDropdownCategoria(data, 'categoryEditRevenueDropdown', 'dropdownCategoryRevenueEdit');
})
.catch(error => console.error('Erro:', error));

function popularDropdownCategoria(valores, id_dropdown, id_dropdowntext) {
    const dropdown = document.getElementById(id_dropdown);

    valores.forEach(item => {
        const option = document.createElement('a');
        option.classList.add('dropdown-item');
        option.textContent = item.nome;
        dropdown.appendChild(option);

        
        option.addEventListener('click', function() {
            const selectedText = this.textContent;
            const dropdownButton = document.getElementById(id_dropdowntext);
            dropdownButton.textContent = selectedText;
            dropdownButton.value = item.categoriaId;
        });
    });
}

function popularDropdown(valores, id_dropdown, id_dropdowntext) {
    const dropdown = document.getElementById(id_dropdown);

    valores.forEach(item => {
        const option = document.createElement('a');
        option.classList.add('dropdown-item');
        option.textContent = item.description;
        dropdown.appendChild(option);

        
        option.addEventListener('click', function() {
            const selectedText = this.textContent;
            const dropdownButton = document.getElementById(id_dropdowntext);
            dropdownButton.textContent = selectedText;
            dropdownButton.value = item.value;
        });
    });
}

function popularDropdownCards(valores, id_dropdown, id_dropdowntext) {
    const dropdown = document.getElementById(id_dropdown);

    valores.forEach(item => {
        const option = document.createElement('a');
        option.classList.add('dropdown-item');
        option.textContent = enumBandeira[item.bandeira] + " - " + item.cardNumber.slice(-4);
        dropdown.appendChild(option);

        
        option.addEventListener('click', function() {
            const selectedText = this.textContent;
            const dropdownButton = document.getElementById(id_dropdowntext);
            dropdownButton.textContent = selectedText;
            dropdownButton.value = item.cardId;
        });
    });
}

const formaPagamentoItems = document.querySelectorAll('#formaPagamentoRevenueAddDropdown');

formaPagamentoItems.forEach(item => {
    item.addEventListener('click', function(event) {
        event.preventDefault();
        const tipoPagamento = document.getElementById('dropdownFormaPagamentoRevenueAdd');
        
        if (parseInt(tipoPagamento.value) === 3) {
            console.log(tipoPagamento.value);
            document.getElementById('cartoesCredito').style.display = 'block';
            document.getElementById('cartoesDebito').style.display = 'none';
            carregarCartoes(0);
        } 
        else if (parseInt(tipoPagamento.value) === 4) 
        {
            document.getElementById('cartoesDebito').style.display = 'block';
            document.getElementById('cartoesCredito').style.display = 'none';
            carregarCartoes(1);
        }else{
            document.getElementById('cartoesDebito').style.display = 'none';
            document.getElementById('cartoesCredito').style.display = 'none';
        }
    });
});

function carregarCartoes(cardType) {
    try {
        fetch(`http://localhost:5134/api/v1/Card/cardtype`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Não foi possível carregar os cartões');
            }
            return response.json();
        })
        .then(data => {
            
            if(cardType === 0){
                popularDropdownCards(data, 'cartaoCreditoRevenueAddDropdown', 'dropdownCartaoCreditoAddRevenue');
            }
            else if(cardType === 1){
                popularDropdownCards(data, 'cartaoDebitoRevenueAddDropdown', 'dropdownCartaoDebitoAddRevenue');
            }
        })
        .catch(error => console.error('Erro:', error));

    } catch (error) {
        console.error('Erro ao carregar os cartões:', error);
    }
}

document.getElementById('saveAddRevenue').addEventListener('click', function() {
    const data = document.getElementById('dataInputRevenueAdd').value;
    const formaPagamento = document.getElementById('dropdownFormaPagamentoRevenueAdd').value;
    const categoria = document.getElementById('dropdownCategoryRevenueAdd').value;
    const valor = document.getElementById('valueRevenueAdd').value;
    const descricao = document.getElementById('descriptionRevenueAdd').value;
    const fornecedor = document.getElementById('fornecedorRevenueAdd').value;
    const cardId = document.getElementById('dropdownCartaoCreditoAddRevenue').value;
    
    // Converte os valores numéricos para o formato correto
    const formattedValor = parseFloat(valor);
    const formattedCategoria = parseInt(categoria);
    const formattedCardId = parseInt(cardId);
    const formattedFormaPagamento = parseInt(formaPagamento);

    const receita = {
        Date: data,
        FormaPagamento: formattedFormaPagamento,
        CategoriaId: formattedCategoria,
        Value: formattedValor,
        Description: descricao,
        CardId: formattedCardId,
        Fornecedor: fornecedor
    };
    fetch('http://localhost:5134/api/v1/Receita', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(receita)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao criar receita');
        }
        return response;
    })
    .then(data => {
        console.log('Receita criada com sucesso:', data);
        $('.addRevenue-modal-lg').modal('hide');
        obterDadosETabela();
        document.getElementById('dataInputRevenueAdd').value = '';
        document.getElementById('dropdownFormaPagamentoRevenueAdd').textContent = 'Selecione';
        document.getElementById('dropdownFormaPagamentoRevenueAdd').value = '';
        document.getElementById('dropdownCategoryRevenueAdd').textContent = 'Selecione';
        document.getElementById('dropdownCategoryRevenueAdd').value = '';
        document.getElementById('valueRevenueAdd').value = '';
        document.getElementById('descriptionRevenueAdd').value = '';
        document.getElementById('fornecedorRevenueAdd').value = '';
        document.getElementById('dropdownCartaoCreditoAddRevenue').textContent = 'Selecione';
        document.getElementById('dropdownCartaoCreditoAddRevenue').value = '';
    })
    .catch(error => {
        console.error('Erro ao criar receita:', error);
        // Tratamento de erro ou feedback para o usuário, se necessário
    });
});

function preencherTabelaReceitas(receitas) {
    const tabelaReceitas = document.getElementById('tabelaRevenues');

    receitas.forEach(receita => {
        const novaLinha = document.createElement('tr');

        const tdValue = document.createElement('td');
        tdValue.textContent = parseInt(receita.value).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });;
        const tdDate = document.createElement('td');
        tdDate.textContent = converterFormatoData(receita.date);
        const tdDescription = document.createElement('td');
        tdDescription.textContent = receita.description;
        const tdFormaPagamento = document.createElement('td');
        tdFormaPagamento.textContent = enumFormaPgt[receita.formaPagamento];
        const tdCategoria = document.createElement('td');
        const tdFornecedor = document.createElement('td');
        tdFornecedor.textContent = receita.fornecedor;

        fetch(`http://localhost:5134/api/v1/Categoria/${receita.categoriaId}`, {
            method: 'GET'
        })
        .then(response => response.json())
        .then(data => {
            tdCategoria.textContent = data.nome;
        })
        .catch(error => {
            console.error('Erro ao obter dados:', error);
        });

        const tdOpcoes = document.createElement('td');
        const iconsContainer = document.createElement('div');
        iconsContainer.classList.add('icons-container');

        // Ícone de edição
        const iconEdit = document.createElement('a');
        const iconeWrench = document.createElement('i');
        iconeWrench.classList.add('fas', 'fa-wrench');
        iconeWrench.style.color = 'rgb(0, 0, 0)';
        iconeWrench.dataset.toggle = 'modal';
        iconeWrench.dataset.target = '.bd-editreceita-modal-lg';
        iconeWrench.dataset.id = receita.receitaId;
        iconEdit.appendChild(iconeWrench);

        // Ícone de exclusão
        const iconDelete = document.createElement('a');
        const iconeTrash = document.createElement('i');
        iconeTrash.classList.add('fas', 'fa-trash');
        iconeTrash.style.color = 'red';
        iconeTrash.dataset.toggle = 'modal';
        iconeTrash.dataset.target = '#apagarReceitaModal';
        iconeTrash.dataset.id = receita.receitaId;
        iconDelete.appendChild(iconeTrash);

        // Adicionar ícones ao container
        iconsContainer.appendChild(iconEdit);
        iconsContainer.appendChild(iconDelete);

        // Adicionar o container de ícones ao <td>
        tdOpcoes.appendChild(iconsContainer);

        novaLinha.appendChild(tdValue);
        novaLinha.appendChild(tdDate);
        novaLinha.appendChild(tdDescription);
        novaLinha.appendChild(tdFormaPagamento);
        novaLinha.appendChild(tdCategoria);
        novaLinha.appendChild(tdFornecedor);
        novaLinha.appendChild(tdOpcoes);

        // Adicionar a linha à tabela
        tabelaReceitas.appendChild(novaLinha);
    });

    $('#dataTableRevenues').DataTable();
}

function obterDadosETabela() {
    const tabelaReceitas = document.getElementById('tabelaRevenues');
    tabelaReceitas.innerHTML = '';

    fetch('http://localhost:5134/api/v1/Receita', {
        method: 'GET'
    })
    .then(response => response.json())
    .then(data => {
        preencherTabelaReceitas(data);
    })
    .catch(error => {
        console.error('Erro ao obter dados:', error);
    });
}

function converterFormatoData(data) {

    const partes = data.split('-');

    if (partes.length === 3) {
        const ano = partes[0];
        const mes = partes[1];
        const dia = partes[2].split('T');

        const dataFormatada = `${dia[0]}/${mes}/${ano}`;
        return dataFormatada;
    }
}

const botaoPesquisar = document.getElementById('buttonSearchRevenue');
botaoPesquisar.addEventListener('click', function(event) {
    event.preventDefault();
    pesquisarRevenue();
});

function pesquisarRevenue() {
    const dataSearch = document.getElementById('dataInputRevenueSearch').value;
    const formaPgmtSearch = document.getElementById('dropdownFormaPagamentoRevenueSearch').value;
    const categoriaSearch = document.getElementById('dropdownCategoryRevenueSearch').value;

    if(dataSearch === "" && formaPgmtSearch === "" && categoriaSearch === "")
    {
        obterDadosETabela();
    }
    else
    {
        let url = `http://localhost:5134/api/v1/Receita/search?`
        
        if (dataSearch) {
            url += `data=${dataSearch}&`;
        }

        if (formaPgmtSearch || formaPgmtSearch === 0) {
            url += `formaPgmt=${formaPgmtSearch}&`;
        }

        if (categoriaSearch || categoriaSearch === 0) {
            url += `categoriaId=${categoriaSearch}&`;
        }

        // Remove o último '&' se não houver parâmetros opcionais
        if (url.endsWith('&')) {
            url = url.slice(0, -1);
        }
        fetch(url, {
            method: 'GET'
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao obter os dados.');
            }
            return response.json();
        })
        .then(data => {
            const tabelaCards = document.getElementById('tabelaRevenues');
            tabelaCards.innerHTML = '';
            preencherTabelaReceitas(data);
        })
        .catch(error => {
            console.error('Erro ao obter dados:', error);
        });
    }
}

$('#apagarReceitaModal').on('show.bs.modal', function (event) {
    const button = $(event.relatedTarget);
    const receitaId = button.data('id'); 

    const apagarButton = $(this).find('#apagarReceita');

    apagarButton.off('click');

    apagarButton.on('click', function () {

        fetch('http://localhost:5134/api/v1/Receita/' + receitaId, {
            method: 'DELETE'
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao excluir receita.');
            }
            $('#apagarReceitaModal').modal('hide');
            obterDadosETabela();

            return response.json;
        })
        .then(data => {

        })
        .catch(error => {
            console.error('Erro ao excluir receita:', error);
            alert('Erro ao excluir receita. Tente novamente.');
        });
    });
});

$('.bd-editreceita-modal-lg').on('show.bs.modal', function (event) {
    const button = $(event.relatedTarget);
    const receitaId = button.data('id');

    const editarButton = $(this).find('#saveEditRevenue');

    editarButton.off('click');

    editarButton.on('click', function () {
        const dados = {
            ReceitaId: receitaId, 
            Value: document.getElementById('valueRevenueEdit').value, 
            Description: document.getElementById('descriptionRevenueEdit').value, 
            Fornecedor: document.getElementById('fornecedorRevenueEdit').value, 
            Date: document.getElementById('dataInputRevenueEdit').value, 
            CategoriaId: document.getElementById('dropdownCategoryRevenueEdit').value, 
            FormaPagamento: document.getElementById('dropdownFormaPagamentoRevenueEdit').value
        }

        const dadosNaoNulos = {};
        Object.keys(dados).forEach(key => {
        if (dados[key] !== null && dados[key] !== '' && dados[key] !== undefined) {
            dadosNaoNulos[key] = dados[key];
        }
        });

        dadosNaoNulos.Value = parseFloat(dadosNaoNulos.Value);
        dadosNaoNulos.CategoriaId = parseInt(dadosNaoNulos.CategoriaId);
        dadosNaoNulos.FormaPagamento = parseInt(dadosNaoNulos.FormaPagamento);
        
        fetch(`http://localhost:5134/api/v1/Receita/${receitaId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dadosNaoNulos)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Ocorreu um erro ao editar a receita.');
            }

            return response.json();
        })
        .then(result => {
            console.log('Receita criada com sucesso:', result);
            $('.bd-editreceita-modal-lg').modal('hide');
            obterDadosETabela();
            document.getElementById('dataInputRevenueEdit').value = '';
            document.getElementById('dropdownCategoryRevenueEdit').textContent = 'Selecione';
            document.getElementById('dropdownCategoryRevenueEdit').value = '';
            document.getElementById('dropdownFormaPagamentoRevenueEdit').textContent = 'Selecione';
            document.getElementById('dropdownFormaPagamentoRevenueEdit').value = '';
            document.getElementById('valueRevenueEdit').value = '';
            document.getElementById('descriptionRevenueEdit').value = '';
            document.getElementById('fornecedorRevenueEdit').value = '';
        })
        .catch(error => {
            console.error('Erro ao editar a receita:', error);
            // Tratar o erro ou fornecer feedback ao usuário, se necessário
        });
    });
});