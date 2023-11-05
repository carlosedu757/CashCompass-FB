using RestAPI.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models
{
    public class Card 
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        
        [DisplayFormat(DataFormatString = "C", ApplyFormatInEditMode = true)]
        public decimal Limit { get; set; }
        
        [DisplayFormat(DataFormatString = "C", ApplyFormatInEditMode = true)]
        public decimal Value { get; set; }
        
        [DisplayFormat(DataFormatString = "dd/MM")]
        public DateOnly DateClose { get; set; }

        public EBandeira Bandeira { get; set; }
        public ECardType CardType { get; set; }
        
        public User User { get; set; }
    }
}
