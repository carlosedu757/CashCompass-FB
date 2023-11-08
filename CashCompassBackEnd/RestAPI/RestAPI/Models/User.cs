using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(60)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string Avatar { get; set; }

        public List<Despesa> Despesas { get; set; } = new List<Despesa>();
        public List<Card> Cards { get; set; } = new List<Card>();
    }
}
