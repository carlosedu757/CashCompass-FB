obterDadosETabela();

const enumQuitacao = {
    0: 'Pendente',
    1: 'Concluída'
}

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

const arrayEnumQuitacao = Object.keys(enumQuitacao).map(key => ({ value: key, description: enumQuitacao[key] }));

popularDropdown(arrayEnumQuitacao, 'quitacaoDespesaSearchDropdown', 'dropdownQuitacaoDespesaSearch')
popularDropdown(arrayEnumQuitacao, 'quitacaoDespesaAddDropdown', 'dropdownQuitacaoDespesaAdd')
popularDropdown(arrayEnumQuitacao, 'quitacaoDespesaEditDropdown', 'dropdownQuitacaoDespesaEdit')

fetch('http://localhost:5134/formapagt')
.then(response => response.json())
.then(data => {
    popularDropdown(data, 'formaPagamentoDespesaSearchDropdown', 'dropdownFormaPagamentoDespesaSearch');
    popularDropdown(data, 'formaPagamentoDespesaAddDropdown', 'dropdownFormaPagamentoDespesaAdd');
    popularDropdown(data, 'formaPagamentoDespesaEditDropdown', 'dropdownFormaPagamentoDespesaEdit');
})
.catch(error => console.error('Erro:', error));

fetch('http://localhost:5134/api/v1/Categoria', {
    method: 'GET'
})
.then(response => response.json())
.then(data => {
    popularDropdownCategoria(data, 'categorySearchDespesaDropdown', 'dropdownCategoryDespesaSearch');
    popularDropdownCategoria(data, 'categoryAddDespesaDropdown', 'dropdownCategoryDespesaAdd');
    popularDropdownCategoria(data, 'categoryEditDespesaDropdown', 'dropdownCategoryDespesaEdit');
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

const formaPagamentoItems = document.querySelectorAll('#formaPagamentoDespesaAddDropdown');

formaPagamentoItems.forEach(item => {
    item.addEventListener('click', function(event) {
        event.preventDefault();
        const tipoPagamento = document.getElementById('dropdownFormaPagamentoDespesaAdd');
        
        if (parseInt(tipoPagamento.value) === 3) {
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
                popularDropdownCards(data, 'cartaoCreditoDespesaAddDropdown', 'dropdownCartaoCreditoAddDespesa');
            }
            else if(cardType === 1){
                popularDropdownCards(data, 'cartaoDebitoDespesaAddDropdown', 'dropdownCartaoDebitoAddDespesa');
            }
        })
        .catch(error => console.error('Erro:', error));

    } catch (error) {
        console.error('Erro ao carregar os cartões:', error);
    }
}

document.getElementById('saveAddDespesa').addEventListener('click', function() {
    const data = document.getElementById('dataInputDespesaAdd').value;
    const formaPagamento = document.getElementById('dropdownFormaPagamentoDespesaAdd').value;
    const categoria = document.getElementById('dropdownCategoryDespesaAdd').value;
    const quitacao = document.getElementById('dropdownQuitacaoDespesaAdd').value;
    const valor = document.getElementById('valueDespesaAdd').value;
    const descricao = document.getElementById('descriptionDespesaAdd').value;
    const numParcelas = document.getElementById('parcelNumberDespesaAdd').value;
    const cardId = document.getElementById('dropdownCartaoCreditoAddDespesa').value;
    
    // Converte os valores numéricos para o formato correto
    const formattedData = (data === "") ? null : data;
    const formattedValor = parseFloat(valor);
    const formattedCategoria = parseInt(categoria);
    const formattedCardId = parseInt(cardId);
    const formattedFormaPagamento = parseInt(formaPagamento);
    const formattedNumParcelas = parseInt(numParcelas);

    const despesa = {
        Date: formattedData,
        FormaPagamento: formattedFormaPagamento,
        CategoriaId: formattedCategoria,
        Value: formattedValor,
        Description: descricao,
        CardId: formattedCardId,
        WasPaid: Boolean(quitacao === "1"),
        NumParcelas: formattedNumParcelas
    };

    fetch('http://localhost:5134/api/v1/Despesa', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(despesa)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao criar despesa');
        }
        return response;
    })
    .then(data => {
        $('.addDespesa-modal-lg').modal('hide');

        obterDadosETabela();
        document.getElementById('dataInputDespesaAdd').value = '';
        document.getElementById('dropdownFormaPagamentoDespesaAdd').textContent = 'Selecione';
        document.getElementById('dropdownFormaPagamentoDespesaAdd').value = '';
        document.getElementById('dropdownCategoryDespesaAdd').textContent = 'Selecione';
        document.getElementById('dropdownCategoryDespesaAdd').value = '';
        document.getElementById('valueDespesaAdd').value = '';
        document.getElementById('descriptionDespesaAdd').value = '';
        document.getElementById('dropdownCartaoCreditoAddDespesa').textContent = 'Selecione';
        document.getElementById('dropdownCartaoCreditoAddDespesa').value = '';
        document.getElementById('parcelNumberDespesaAdd').value = '';
        document.getElementById('dropdownQuitacaoDespesaAdd').textContent = 'Selecione';
        document.getElementById('dropdownQuitacaoDespesaAdd').value = '';
    })
    .catch(error => {
        console.error('Erro ao criar receita:', error);
        // Tratamento de erro ou feedback para o usuário, se necessário
    });
});

function preencherTabelaDespesas(despesas) {
    const tabelaReceitas = document.getElementById('tabelaDespesas');

    despesas.forEach(despesa => {
        const novaLinha = document.createElement('tr');
        
        const tdValue = document.createElement('td');
        tdValue.textContent = parseInt(despesa.value).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });;
        const tdDate = document.createElement('td');
        tdDate.textContent = converterFormatoData(despesa.date);
        const tdDescription = document.createElement('td');
        tdDescription.textContent = despesa.description;
        const tdFormaPagamento = document.createElement('td');
        tdFormaPagamento.textContent = enumFormaPgt[despesa.formaPagamento];
        const tdCategoria = document.createElement('td');
        const tdNumParcel = document.createElement('td');
        tdNumParcel.textContent = despesa.numParcelas;
        const tdQuitacao = document.createElement('td');
        tdQuitacao.textContent = despesa.wasPaid === false ? enumQuitacao[0] : enumQuitacao[1];

        fetch(`http://localhost:5134/api/v1/Categoria/${despesa.categoriaId}`, {
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
        iconeWrench.dataset.target = '.bd-editdespesa-modal-lg';
        iconeWrench.dataset.id = despesa.despesaId;
        iconEdit.appendChild(iconeWrench);

        // Ícone de exclusão
        const iconDelete = document.createElement('a');
        const iconeTrash = document.createElement('i');
        iconeTrash.classList.add('fas', 'fa-trash');
        iconeTrash.style.color = 'red';
        iconeTrash.dataset.toggle = 'modal';
        iconeTrash.dataset.target = '#apagarDespesaModal';
        iconeTrash.dataset.id = despesa.despesaId;
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
        novaLinha.appendChild(tdNumParcel);
        novaLinha.appendChild(tdQuitacao);
        novaLinha.appendChild(tdOpcoes);

        // Adicionar a linha à tabela
        tabelaReceitas.appendChild(novaLinha);
    });

    $('#dataTableDespesas').DataTable();
}

function obterDadosETabela() {
    const tabelaDespesas = document.getElementById('tabelaDespesas');
    tabelaDespesas.innerHTML = '';

    fetch('http://localhost:5134/api/v1/Despesa', {
        method: 'GET'
    })
    .then(response => response.json())
    .then(data => {
        preencherTabelaDespesas(data);
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

const botaoPesquisar = document.getElementById('btnDespesaSearch');
botaoPesquisar.addEventListener('click', function(event) {
    event.preventDefault();
    pesquisarDespesa();
});

function pesquisarDespesa() {
    const dataSearch = document.getElementById('dataInputDespesaSearch').value;
    const formaPgmtSearch = document.getElementById('dropdownFormaPagamentoDespesaSearch').value;
    const categoriaSearch = document.getElementById('dropdownCategoryDespesaSearch').value;
    const quitacaoSearch = document.getElementById('dropdownQuitacaoDespesaSearch').value;

    if(dataSearch === "" && formaPgmtSearch === "" && categoriaSearch === "" && quitacaoSearch === "")
    {
        obterDadosETabela();
    }
    else
    {
        let url = `http://localhost:5134/api/v1/Despesa/search?`
        
        if (dataSearch) {
            url += `data=${dataSearch}&`;
        }

        if (formaPgmtSearch || formaPgmtSearch === 0) {
            url += `formaPgmt=${formaPgmtSearch}&`;
        }

        if (categoriaSearch || categoriaSearch === 0) {
            url += `categoriaId=${categoriaSearch}&`;
        }

        if (quitacaoSearch || quitacaoSearch === 0) {
            url += `wasPaid=${Boolean(quitacaoSearch === "1")}&`;
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
            const tabelaCards = document.getElementById('tabelaDespesas');
            tabelaCards.innerHTML = '';
            preencherTabelaDespesas(data);
        })
        .catch(error => {
            console.error('Erro ao obter dados:', error);
        });
    }
}

$('#apagarDespesaModal').on('show.bs.modal', function (event) {
    const button = $(event.relatedTarget);
    const despesaId = button.data('id'); 
    
    const apagarButton = $(this).find('#apagarDespesa');

    apagarButton.off('click');
    
    apagarButton.on('click', function () {
        console.log('alo');
        fetch('http://localhost:5134/api/v1/Despesa/' + despesaId, {
            method: 'DELETE'
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao excluir despesa.');
            }
            $('#apagarDespesaModal').modal('hide');
            obterDadosETabela();

            return response.json;
        })
        .then(data => {

        })
        .catch(error => {
            console.error('Erro ao excluir despesa:', error);
            alert('Erro ao excluir despesa. Tente novamente.');
        });
    });
});

$('.bd-editdespesa-modal-lg').on('show.bs.modal', function (event) {
    const button = $(event.relatedTarget);
    const despesaId = button.data('id');

    const editarButton = $(this).find('#saveEditDespesa');

    editarButton.off('click');

    editarButton.on('click', function () {
        const dados = {
            DespesaId: despesaId, 
            Value: document.getElementById('valueDespesaEdit').value, 
            Description: document.getElementById('descriptionDespesaEdit').value, 
            WasPaid: document.getElementById('dropdownQuitacaoDespesaEdit').value, 
            Date: document.getElementById('dataInputDespesaEdit').value,
            CategoriaId: document.getElementById('dropdownCategoryDespesaEdit').value, 
            FormaPagamento: document.getElementById('dropdownFormaPagamentoDespesaEdit').value,
            NumParcelas: document.getElementById('parcelNumberDespesaEdit').value
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
        dadosNaoNulos.NumParcelas = parseInt(dadosNaoNulos.NumParcelas);
        dadosNaoNulos.WasPaid = dadosNaoNulos.WasPaid !== '' ? Boolean(dadosNaoNulos.WasPaid === "1") : null;
        
        fetch(`http://localhost:5134/api/v1/Despesa/${despesaId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dadosNaoNulos)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Ocorreu um erro ao editar a despesa.');
            }

            return response.json();
        })
        .then(result => {
            $('.bd-editdespesa-modal-lg').modal('hide');
            obterDadosETabela();

            document.getElementById('dataInputDespesaEdit').value = '';
            document.getElementById('dropdownCategoryDespesaEdit').textContent = 'Selecione';
            document.getElementById('dropdownCategoryDespesaEdit').value = '';
            document.getElementById('dropdownFormaPagamentoDespesaEdit').textContent = 'Selecione';
            document.getElementById('dropdownFormaPagamentoDespesaEdit').value = '';
            document.getElementById('valueDespesaEdit').value = '';
            document.getElementById('descriptionDespesaEdit').value = '';
            document.getElementById('parcelNumberDespesaEdit').value = '';
            document.getElementById('dropdownQuitacaoDespesaEdit').textContent = 'Selecione';
            document.getElementById('dropdownQuitacaoDespesaEdit').value = '';
        })
        .catch(error => {
            console.error('Erro ao editar a despesa:', error);
            // Tratar o erro ou fornecer feedback ao usuário, se necessário
        });
    });
});