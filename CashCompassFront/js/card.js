// Função para preencher o dropdown com valores do enum
function popularDropdownCardType(valores) {
    const dropdown = document.getElementById('cardTypeDropdown');

    valores.forEach(item => {
        const option = document.createElement('a');
        option.classList.add('dropdown-item');
        option.href = '#';  // Adicione a URL correta se necessário
        option.textContent = item.description;
        dropdown.appendChild(option);
    });
}

// Requisição AJAX para obter valores do enum CardType
fetch('http://localhost:5134/cardtypes')
    .then(response => response.json())
    .then(data => {
        // Preencher o dropdown com os valores do enum
        popularDropdownCardType(data);
    })
    .catch(error => console.error('Erro:', error));


function popularDropdownBandeira(valores) {
    const dropdown = document.getElementById('bandeiraDropdown');

    valores.forEach(item => {
        const option = document.createElement('a');
        option.classList.add('dropdown-item');
        option.href = '#';  // Adicione a URL correta se necessário
        option.textContent = item.description;
        dropdown.appendChild(option);
    });
}
// Requisição AJAX para obter valores do enum Bandeira
fetch('http://localhost:5134/bandeiras')
    .then(response => response.json())
    .then(data => {
        // Preencher o dropdown com os valores do enum
        popularDropdownBandeira(data);
    })
    .catch(error => console.error('Erro:', error));
