const express = require('express');
const app = express();
const PORT = 3000;

app.use(express.json());

// Importar as rotas das despesas
const expensesRouter = require('./routes/expenses');
app.use('/expenses', expensesRouter);

app.listen(PORT, () => {
  console.log(`Servidor rodando na porta ${PORT}`);
});