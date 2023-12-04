obterDadosETabela();

const enumTipo = {
    0: 'Crédito',
    1: 'Débito'
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

fetch('http://localhost:5134/cardtypes')
    .then(response => response.json())
    .then(data => {
        popularDropdown(data, 'cardTypeFilterDropdown', 'dropdownFilterCardTypeText');
        popularDropdown(data, 'cardTypeAddCardDropdown', 'dropdownAddCardCardTypeText');
        popularDropdown(data, 'cardTypeEditCardDropdown', 'dropdownEditCardCardTypeButton');
    })
    .catch(error => console.error('Erro:', error));

fetch('http://localhost:5134/bandeiras')
    .then(response => response.json())
    .then(data => {
        popularDropdown(data, 'bandeiraFilterDropdown', 'dropdownFilterBandeiraText');
        popularDropdown(data, 'bandeiraAddCardDropdown', 'dropdownAddCardBandeiraText');
        popularDropdown(data, 'bandeiraEditCardDropdown', 'dropdownEditCardBandeiraButton');
    })
    .catch(error => console.error('Erro:', error));


function popularDropdown(valores, id_dropdown, id_dropdowntext) {
    const dropdown = document.getElementById(id_dropdown);

    valores.forEach(item => {
        const option = document.createElement('a');
        option.classList.add('dropdown-item');
        option.href = '#';
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

document.getElementById('saveAddCard').addEventListener('click', function() {
    const numberCard = document.getElementById('numberCardCreate').value;
    const cardType = document.getElementById('dropdownAddCardCardTypeText').value;
    const bandeira = document.getElementById('dropdownAddCardBandeiraText').value;
    const limitValue = document.getElementById('limitValueCreate').value;
    const currentValue = document.getElementById('currentValueCreate').value;
    const closingDay = document.getElementById('closingDayCardCreate').value;

    // Validações
    if (!numberCard || !cardType || !bandeira) {
        console.error('Preencha todos os campos obrigatórios.');
        return; // Aborta o envio se campos obrigatórios não estiverem preenchidos
    }

    // Converte os valores numéricos para o formato correto
    const formattedLimitValue = parseFloat(limitValue);
    const formattedCurrentValue = parseFloat(currentValue);
    const formattedClosingDay = parseInt(closingDay);
    const formattedCardType = parseInt(cardType);
    const formattedBandeira = parseInt(bandeira);

    const cardData = {
        CardNumber: numberCard,
        Type: formattedCardType,
        Bandeira: formattedBandeira,
        LimitValue: isNaN(formattedLimitValue) ? 0 : formattedLimitValue,
        CurrentValue: isNaN(formattedCurrentValue) ? 0 : formattedCurrentValue,
        DateClose: isNaN(formattedClosingDay) ? 0 : formattedClosingDay
    };
    console.log(cardData);
    fetch('http://localhost:5134/api/v1/Card', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(cardData)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao criar cartão');
        }
        return response.json();
    })
    .then(data => {
        console.log('Cartão criado com sucesso:', data);
        $('.addCard-modal-lg').modal('hide');
        obterDadosETabela();
        document.getElementById('numberCardCreate').value = '';
        document.getElementById('dropdownAddCardCardTypeText').textContent = 'Selecione';
        document.getElementById('dropdownAddCardCardTypeText').value = '';
        document.getElementById('dropdownAddCardBandeiraText').textContent = 'Selecione';
        document.getElementById('dropdownAddCardBandeiraText').value = '';
        document.getElementById('limitValueCreate').value = '';
        document.getElementById('currentValueCreate').value = '';
        document.getElementById('closingDayCardCreate').value = '';
    })
    .catch(error => {
        console.error('Erro ao criar cartão:', error);
        // Tratamento de erro ou feedback para o usuário, se necessário
    });
});

function obterDadosETabela() {
    const tabelaCards = document.getElementById('tabelaCards');
    tabelaCards.innerHTML = '';

    fetch('http://localhost:5134/api/v1/Card', {
        method: 'GET'
    })
    .then(response => response.json())
    .then(data => {
        preencherTabelaCard(data);
    })
    .catch(error => {
        console.error('Erro ao obter dados:', error);
    });
}

function preencherTabelaCard(cards) {
    const tabelaCards = document.getElementById('tabelaCards');

    cards.forEach(card => {
        // Criar uma nova linha <tr>
        const novaLinha = document.createElement('tr');

        const tdNumCard = document.createElement('td');
        tdNumCard.textContent = "************" + card.cardNumber.slice(-4);
        const tdCardType = document.createElement('td');
        tdCardType.textContent = enumTipo[card.type];
        const tdBandeira = document.createElement('td');
        tdBandeira.textContent = enumBandeira[card.bandeira];
        const tdLimite = document.createElement('td');
        tdLimite.textContent = card.limitValue;
        const tdCurrentValue = document.createElement('td');
        tdCurrentValue.textContent = card.currentValue;
        const tdClosedDay = document.createElement('td');
        tdClosedDay.textContent = card.dateClose;

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
        iconeWrench.dataset.target = '.editcard-modal-lg';
        iconeWrench.dataset.id = card.cardId;
        iconEdit.appendChild(iconeWrench);

        // Ícone de exclusão
        const iconDelete = document.createElement('a');
        const iconeTrash = document.createElement('i');
        iconeTrash.classList.add('fas', 'fa-trash');
        iconeTrash.style.color = 'red';
        iconeTrash.dataset.toggle = 'modal';
        iconeTrash.dataset.target = '#apagarCardModal';
        iconeTrash.dataset.id = card.cardId;
        iconDelete.appendChild(iconeTrash);

        // Adicionar ícones ao container
        iconsContainer.appendChild(iconEdit);
        iconsContainer.appendChild(iconDelete);

        // Adicionar o container de ícones ao <td>
        tdOpcoes.appendChild(iconsContainer);

        // Adicionar os <td>s à linha <tr>
        novaLinha.appendChild(tdNumCard);
        novaLinha.appendChild(tdCardType);
        novaLinha.appendChild(tdBandeira);
        novaLinha.appendChild(tdLimite);
        novaLinha.appendChild(tdCurrentValue);
        novaLinha.appendChild(tdClosedDay);
        novaLinha.appendChild(tdOpcoes);

        // Adicionar a linha à tabela
        tabelaCards.appendChild(novaLinha);
    });

    $('#dataTableCard').DataTable();
}

function pesquisarCard() {
    const numberCardSearch = document.getElementById('numberCardSeach').value;
    const cardTypeSearch = document.getElementById('dropdownFilterCardTypeText').value;
    const bandeiraSearch = document.getElementById('dropdownFilterBandeiraText').value;
    const closedDateSearch = document.getElementById('closingDaySeach').value;
    if(numberCardSearch === "" && cardTypeSearch === "" && bandeiraSearch === "" && closedDateSearch === "")
    {
        obterDadosETabela();
    }
    else
    {
        let url = `http://localhost:5134/api/v1/Card/search?`
        
        if (numberCardSearch) {
            url += `cardNumber=${numberCardSearch}&`;
        }

        if (cardTypeSearch || cardTypeSearch === 0) {
            url += `cardType=${cardTypeSearch}&`;
        }

        if (bandeiraSearch || bandeiraSearch === 0) {
            url += `bandeira=${bandeiraSearch}&`;
        }

        if (closedDateSearch) {
            url += `closedDate=${closedDateSearch}&`;
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
            const tabelaCards = document.getElementById('tabelaCards');
            tabelaCards.innerHTML = '';
            preencherTabelaCard(data);
        })
        .catch(error => {
            console.error('Erro ao obter dados:', error);
        });
    }
}

const botaoPesquisar = document.getElementById('buttonSearchCard');
botaoPesquisar.addEventListener('click', function(event) {
    event.preventDefault();
    pesquisarCard();
});

$('#apagarCardModal').on('show.bs.modal', function (event) {
    const button = $(event.relatedTarget);
    const cardId = button.data('id'); 

    const apagarButton = $(this).find('#apagarCard');

    apagarButton.off('click');

    apagarButton.on('click', function () {

        fetch('http://localhost:5134/api/v1/Card/' + cardId, {
            method: 'DELETE'
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao excluir cartão.');
            }
            $('#apagarCardModal').modal('hide');
            obterDadosETabela();

            return response.json;
        })
        .then(data => {

        })
        .catch(error => {
            console.error('Erro ao excluir cartão:', error);
            alert('Erro ao excluir cartão. Tente novamente.');
        });
    });
});

$('.editcard-modal-lg').on('show.bs.modal', function (event) {
    const button = $(event.relatedTarget);
    const cardId = button.data('id');

    const editarButton = $(this).find('#saveEditCard');

    editarButton.off('click');

    editarButton.on('click', function () {
        const dados = {
            CardId: cardId, 
            CardNumber: document.getElementById('numberCardEdit').value, 
            LimitValue: document.getElementById('limitValueEdit').value, 
            CurrentValue: document.getElementById('currentValueEdit').value, 
            DateClose: document.getElementById('closingDayCardEdit').value, 
            Bandeira: document.getElementById('dropdownEditCardBandeiraButton').value, 
            Type: document.getElementById('dropdownEditCardCardTypeButton').value
        }

        const dadosNaoNulos = {};
        Object.keys(dados).forEach(key => {
        if (dados[key] !== null && dados[key] !== '' && dados[key] !== undefined) {
            dadosNaoNulos[key] = dados[key];
        }
        });

        dadosNaoNulos.LimitValue = parseFloat(dadosNaoNulos.LimitValue);
        dadosNaoNulos.CurrentValue = parseFloat(dadosNaoNulos.CurrentValue);
        dadosNaoNulos.DateClose = parseInt(dadosNaoNulos.DateClose);
        dadosNaoNulos.Type = parseInt(dadosNaoNulos.Type);
        dadosNaoNulos.Bandeira = parseInt(dadosNaoNulos.Bandeira);
        
        fetch(`http://localhost:5134/api/v1/Card/${cardId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dadosNaoNulos)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Ocorreu um erro ao editar o cartão.');
            }
            $('.editcard-modal-lg').modal('hide');
            obterDadosETabela();

            return response.json();
        })
        .then(result => {
            console.log('Cartão editado com sucesso:', result);
            document.getElementById('numberCardEdit').value = '';
            document.getElementById('dropdownEditCardCardTypeButton').textContent = 'Selecione';
            document.getElementById('dropdownEditCardCardTypeButton').value = '';
            document.getElementById('dropdownEditCardBandeiraButton').textContent = 'Selecione';
            document.getElementById('dropdownEditCardBandeiraButton').value = '';
            document.getElementById('limitValueEdit').value = '';
            document.getElementById('currentValueEdit').value = '';
            document.getElementById('closingDayCardEdit').value = '';
        })
        .catch(error => {
            console.error('Erro ao editar o cartão:', error);
            // Tratar o erro ou fornecer feedback ao usuário, se necessário
        });
    });
});