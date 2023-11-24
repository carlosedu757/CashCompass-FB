// Requisição AJAX para obter valores do enum CardType
fetch('http://localhost:5134/cardtypes')
    .then(response => response.json())
    .then(data => {
        // Preencher o dropdown com os valores do enum
        popularDropdown(data, 'cardTypeFilterDropdown', 'dropdownFilterCardTypeText');
        popularDropdown(data, 'cardTypeAddCardDropdown', 'dropdownAddCardCardTypeText');
    })
    .catch(error => console.error('Erro:', error));

// Requisição AJAX para obter valores do enum Bandeira
fetch('http://localhost:5134/bandeiras')
    .then(response => response.json())
    .then(data => {
        // Preencher o dropdown com os valores do enum
        popularDropdown(data, 'bandeiraFilterDropdown', 'dropdownFilterBandeiraText');
        popularDropdown(data, 'bandeiraAddCardDropdown', 'dropdownAddCardBandeiraText');
    })
    .catch(error => console.error('Erro:', error));

// Função para preencher o dropdown com valores do enum
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
        });
    });
}

