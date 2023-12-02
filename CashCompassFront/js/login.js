document.getElementById('btnLogin').addEventListener('click', function (event) {
    event.preventDefault();

    const email = document.getElementById('exampleInputEmail').value;
    const password = document.getElementById('exampleInputPassword').value;

    const userData = {
        Email: email,
        Password: password
    };
    
    fetch('http://localhost:5134/api/v1/User/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao fazer login');
        }
        return response.json();
    })
    .then(data => {
        console.log('Login bem-sucedido:', data);
        window.location.href = './index.html';
    })
    .catch(error => {
        console.error('Erro ao fazer login:', error);
        
        errorMessage.style.display = 'block';
        setTimeout(() => {
            errorMessage.style.display = 'none';
        }, 10000);
    });
});