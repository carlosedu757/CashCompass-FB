using RestAPI.Models.Enum;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Models;

public class Card 
{
    public Card()
    {
        Despesas = new Collection<Despesa>();
    }

    [Key]
    public int CardId { get; set; }

    [Required]
    [StringLength(16)]
    public string CardNumber { get; set; }
    
    [DisplayFormat(DataFormatString = "C", ApplyFormatInEditMode = true)]
    public decimal LimitValue { get; set; }

    
    [DisplayFormat(DataFormatString = "C", ApplyFormatInEditMode = true)]
    public decimal CurrentValue { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "dd/MM")]
    public DateOnly DateClose { get; set; }

    [Required]
    public Bandeira Bandeira { get; set; }

    [Required]
    public CardType Type { get; set; }

    public ICollection<Despesa>? Despesas { get; set; }
    //public List<Despesa> Despesas { get; set; } = new List<Despesa>();

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}
