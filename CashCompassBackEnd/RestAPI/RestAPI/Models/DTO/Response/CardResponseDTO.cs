using RestAPI.Models.Enum;

namespace RestAPI.Models.DTO.Response;

public class CardResponseDTO
{

    public CardResponseDTO(Card card)
    {
        UserName = card.User.Name;
        Number = card.Number;
        Limit = card.Limit;
        Value = card.Value;
        DateClose = card.DateClose;
        Bandeira = card.Bandeira;
        CardType = card.CardType;
    }
    
    public string UserName { get; set; }
    
    public string Number { get; set; }
    
    public decimal Limit { get; set; }
    
    public decimal Value { get; set; }
    
    public DateOnly DateClose { get; set; }

    public EBandeira Bandeira { get; set; }
    
    public ECardType CardType { get; set; }
    
}