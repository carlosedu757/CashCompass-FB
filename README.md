# CashCompass-API 💰⏱
![Em Desenvolvimento](http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=yellow&style=for-the-badge)

## Requisitos da API 📃📌
---
* Programa de controle financeiro. É fornecida a receita do usuário para o mês, e a cada nova despesa adicionada, o valor 
disponível de receita no mês pelo usuário é atualizado. Uma conta de usuário é necessária para usar o programa. A 
conta terá nome completo, email e avatar. Uma despesa tem os seguintes dados:

  * Valor <br>
  * Data <br>
  * Descrição <br>
  * Categoria (casa, carro, alimentação, animal de estimação, etc) <br>
  * Forma de pagamento (cartão de débito, cartão de crédito ou dinheiro) <br>
  * Número de parcelas (apenas para cartão de crédito) <br>
  * Se está paga ou não <br>

*As informações dos cartões de crédito e débito seguirão o exemplo da tabela seguinte*

| Numero | Tipo | Bandeira | Limite (R$) | Valor Atual (R$) | Dia Fechamento |
| ------------- | ------------- |  ------------- |  ------------- |  ------------- |  ------------- |
| 1122 1833 | Credito | Cielo | 3000 | 0 | 28 |
| 2347 9326 | Credito | Mastercard | 1500 | 0 | 24 |
| 5558 0991 | Debito | Visa | Receita do Mes | - | - |
| 0933 4967 | Debito | Hipercard | Receita do Mes | - | - |

---
### ⚙ As funcionalidades do programa são:
 - Criação de conta;
 - Login;
 - Logout;
 - Edição de conta;
 - Remoção de conta;
 - CRUD de receita;
 - CRUD de cartão de crédito;
 - CRUD de cartão de débito;
 - CRUD de categoria. Uma categoria tem apenas o nome;
---

### ↘ CRUD de despesa
• Na adição de despesa verificações são obrigatórias. Se a despesa for em cartão de
crédito, é necessário verificar se a despesa não ultrapassará o valor limite do cartão e se
não ultrapassou o dia de fechamento da fatura, para inclusão da despesa na fatura do
mês atual ou do mês seguinte. Caso a despesa seja em cartão de débito, deve-se verificar
se ainda existe saldo no cartão (receita do mês) <br><br>
• Nas despesas pagas em cartão de crédito, o valor da prestação será incluído automaticamente
nas faturas seguintes de acordo com o número de prestações em que a despesa é
dividida

_Nas listagens de cada CRUD deverão existir opções para reordenação ou filtragem dos itens.
Por exemplo, as despesas listadas podem ser reordenadas pelo valor, data, categoria, etc. Além
disso, as despesas são filtráveis pelo mês, categoria, etc._

* O grupo deve elaborar os seguintes diagramas UML do sistema: <br>
 • classe <br>
 • pacote <br>
 • caso de uso <br>
 • sequência <br>
 • máquina de estados ou transição de estados <br>

_Em seguida, implementar (ou reestruturar) o código do sistema utilizando os princípios SOLID da
orientação a objetos_

<h2>👨‍💻 Autores</h2>

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/theJoseAlan">
        <img src="https://avatars.githubusercontent.com/u/117518719?v=4" width="100px;" alt="Foto do Jose Alan no GitHub"/><br>
        <sub>
          <b>Jose Alan</b>
        </sub>
      </a>
    </td>
  </tr>
</table>

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/carlosedu757">
        <img src="https://avatars.githubusercontent.com/u/74271104?v=4" width="100px;" alt="Foto do Carlos Eduardo no GitHub"/><br>
        <sub>
          <b>Carlos Eduardo</b>
        </sub>
      </a>
    </td>
  </tr>
</table>

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/FireBlastL">
        <img src="https://github.com/carlosedu757/CashCompass-API/assets/117518719/7c42fdc7-a357-4600-a9d0-431614c1ce9e" width="100px;" alt="Foto do Gabriel Farias no GitHub"/><br>
        <sub>
          <b>Gabriel Farias</b>
        </sub>
      </a>
    </td>
  </tr>
</table>
