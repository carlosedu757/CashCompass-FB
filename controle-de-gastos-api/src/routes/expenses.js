const express = require('express');
const router = express.Router();

// Exemplo de lista de despesas (simulando um banco de dados temporário)
let expenses = [];

router.get('/', (req, res) => {
  res.json(expenses);
});

router.post('/', (req, res) => {
  const newExpense = req.body;
  expenses.push(newExpense);
  res.status(201).json(newExpense);
});

module.exports = router;