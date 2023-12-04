using RestAPI.Models.Enum;

namespace RestAPI.Models;

public class CardDTO
{
    public int CardId { get; set; }

    public string? CardNumber { get; set; }

    public decimal? LimitValue { get; set; }

    public decimal? CurrentValue { get; set; }

    public long? DateClose { get; set; }

    public Bandeira? Bandeira { get; set; }

    public CardType? Type { get; set; }
}