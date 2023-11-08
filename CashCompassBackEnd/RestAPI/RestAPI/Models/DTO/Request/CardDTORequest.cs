using System.ComponentModel.DataAnnotations;
using RestAPI.Models.Enum;

namespace RestAPI.Models.DTO;

public class CardDTORequest
{
    
    [Range(0, double.MaxValue)]
    public decimal Limite { get; set; }
    
    public DateOnly DateClose { get; set; }
    public EBandeira Bandeira { get; set; }
    public ECardType CardType { get; set; }
}