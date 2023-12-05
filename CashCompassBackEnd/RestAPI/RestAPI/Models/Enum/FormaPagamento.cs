using System.ComponentModel;

namespace RestAPI.Models.Enum
{
    public enum FormaPagamento
    {
        [Description("Dinheiro")]
        Dinheiro = 0,
        [Description("Pix")]
        Pix = 1,
        [Description("Transferência Bancária")]
        Transfer = 2,
        [Description("Cartão de Crédito")]
        Credit = 3,
        [Description("Cartão de Débito")]
        Debit = 4
    }
}
