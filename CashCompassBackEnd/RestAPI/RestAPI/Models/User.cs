using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    
    [MaxLength(60)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string Avatar { get; set; }

    public ICollection<Card>? Cards { get; set; }
    //public List<Card> Cards { get; set; } = new List<Card>();
}
