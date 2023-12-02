document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('btnRegister').addEventListener('click', function () {
        const firstName = document.getElementById('exampleFirstName').value;
        const lastName = document.getElementById('exampleLastName').value;
        const email = document.getElementById('exampleInputEmail').value;
        const password = document.getElementById('exampleInputPassword').value;
        const repeatPassword = document.getElementById('exampleRepeatPassword').value;

        if(firstName === "" || lastName === "" || email === "" || password === "" || repeatPassword === "") {
            showMessage('Preencha todos os campos.', 'text-danger');
            return;
        }

        if(!validarEmail(email)){
            errorEmailRegister.style.display = 'block';
            setTimeout(() => {
                errorEmailRegister.style.display = 'none';
            }, 10000);
            return;
        }

        if(!verificarSenha(password)){
            showMessage('A senha deve conter pelo menos 6 caracteres, 1 letra maiúscula, 1 letra minúscula e 1 caractere especial.', 'text-danger');
            return;
        }

        if (password !== repeatPassword) {
            // Caso as senhas não correspondam, exiba uma mensagem de erro
            showMessage('As senhas não correspondem.', 'text-danger');
            return;
        }

        const userData = {
            Name: firstName + " " + lastName,
            Email: email,
            Password: password
        };

        fetch('http://localhost:5134/api/v1/User/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userData)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao registrar usuário.');
            }
            return response.json();
        })
        .then(data => {
            // Registro bem-sucedido
            showMessage('Usuário registrado com sucesso!', 'text-success');
            // Redirecionar para a página de login, por exemplo
            window.location.href = './login.html';
        })
        .catch(error => {
            showMessage(error.message, 'text-danger');
        });
    });
});

function showMessage(message, type) {
    errorMessageRegister.textContent = message;
    errorMessageRegister.classList.add(type);
    errorMessageRegister.style.display = 'block';
    setTimeout(() => {
        errorMessageRegister.style.display = 'none';
    }, 10000);
}

function validarEmail(email) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}

function verificarSenha(senha) {
    const regexSenha = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$/;

    return regexSenha.test(senha);
}