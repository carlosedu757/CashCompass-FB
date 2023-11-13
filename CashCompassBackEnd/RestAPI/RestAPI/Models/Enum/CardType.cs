using System.ComponentModel;

namespace RestAPI.Models.Enum;
public enum CardType
{
    [Description("Crédito")]
    Credit = 0,
    [Description("Débito")]
    Debit = 1
}