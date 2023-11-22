using RestAPI.Models.Enum;

namespace RestAPI.Models.DTO.Request;

public class CardRequestDTO
{
    
    public decimal Limite { get; set; }
    public string CardNumber { get; set; }
    public DateOnly DateClose { get; set; }
    public Bandeira Bandeira { get; set; }
    public CardType CardType { get; set; }
}