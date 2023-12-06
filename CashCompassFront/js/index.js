fetch('http://localhost:5134/api/v1/Despesa/TotalValue')
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao obter o valor total das despesas.');
        }
        return response.json();
    })
    .then(data => {
        const value = data.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }); // Formata para a moeda brasileira
        const valueAllDespesas = document.getElementById('valueAllDespesas');
        valueAllDespesas.textContent = value;
    })
    .catch(error => {
        console.error('Erro ao obter o valor total das despesas:', error);
    });

fetch('http://localhost:5134/api/v1/Receita/TotalValue')
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao obter o valor total das receitas.');
        }
        return response.json();
    })
    .then(data => {
        const value = data.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }); // Formata para a moeda brasileira
        const valueAllReceitas = document.getElementById('valueAllReceitas');
        valueAllReceitas.textContent = value;
    })
    .catch(error => {
        console.error('Erro ao obter o valor total das receitas:', error);
    });


fetch('http://localhost:5134/api/v1/Receita/TotalValueGeral')
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao obter o valor total das receitas.');
        }
        return response.json();
    })
    .then(data => {
        const value = data.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }); // Formata para a moeda brasileira
        const totalValueMoney = document.getElementById('totalValueMoney');
        totalValueMoney.textContent = value;
    })
    .catch(error => {
        console.error('Erro ao obter o valor total das receitas:', error);
    });


    fetch('http://localhost:5134/api/v1/Categoria/GanhosPorCategoria')
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao obter os dados de ganhos por categoria.');
        }
        return response.json();
    })
    .then(data => {
        console.log(data);
        const labels = data.map(item => item.categoria);
        const values = data.map(item => item.valor);

        const ctx = document.getElementById('meuGrafico').getContext('2d');

        // Geração de cores dinâmicas
        const dynamicColors = () => {
            const colors = [];
            for (let i = 0; i < labels.length; i++) {
                const color = `rgba(${Math.floor(Math.random() * 256)}, ${Math.floor(Math.random() * 256)}, ${Math.floor(Math.random() * 256)}, 0.8)`;
                colors.push(color);
            }
            return colors;
        };

        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: labels,
                datasets: [{
                    data: values,
                    backgroundColor: dynamicColors(), // Cores dinâmicas
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                legend: {
                    display: true,
                    position: 'bottom'
                },
                layout: {
                    padding: {
                        left: 15,
                        right: 15,
                        top: 15,
                        bottom: 15
                    }
                }
            }
        });
    })
    .catch(error => {
        console.error('Erro ao obter os dados de ganhos por categoria:', error);
    });
