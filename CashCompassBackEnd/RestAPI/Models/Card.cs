using RestAPI.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models
{
    public class Card 
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Limit { get; set; }
        public decimal Value { get; set; }
        public DateOnly DateClose { get; set; }

        public EBandeira Bandeira { get; set; }
        public ECardType CardType { get; set; }
        public User User { get; set; }
    }
}
