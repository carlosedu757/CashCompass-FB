using RestAPI.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models;

public class Card 
{

    [Key]
    public int CardId { get; set; }

    [Required]
    [StringLength(16)]
    public string CardNumber { get; set; }
        
    public decimal LimitValue { get; set; }
        
    public decimal CurrentValue { get; set; }

    public long DateClose { get; set; }

    [Required]
    public Bandeira Bandeira { get; set; }

    [Required]
    public CardType Type { get; set; }
}