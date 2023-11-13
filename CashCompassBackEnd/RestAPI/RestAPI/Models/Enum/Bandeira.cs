using System.ComponentModel;

namespace RestAPI.Models.Enum
{
    public enum Bandeira
    {
        [Description("VISA")]
        Visa = 0,
        [Description("MASTERCARD")]
        Mastercard = 1,
        [Description("ELO")]
        Elo = 2,
        [Description("AMERICANEXPRESS (AMEX)")]
        AmericanExpress = 3,
        [Description("DINERS CLUB INTERNATIONAL")]
        DinersClubInternational = 4,
        [Description("HIPERCARD")]
        Hipercard = 5,
        [Description("AURA")]
        Aura = 6,
        [Description("HIPER")]
        Hiper = 7,
        [Description("CABAL")]
        Cabal = 8,
        [Description("SOROCRED")]
        Sorocred = 9
    }
}
