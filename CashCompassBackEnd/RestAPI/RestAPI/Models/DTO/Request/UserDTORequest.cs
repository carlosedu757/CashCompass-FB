using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTO;

public class UserDTORequest
{
    [MinLength(4, ErrorMessage = "O tamanho mínimo é de 4 caracteres")]
    [MaxLength(60, ErrorMessage = "O tamanho máximo é de 60 caracteres")]
    public string Name { get; set; }
    
    [EmailAddress(ErrorMessage = "Email inválido !")]
    [MaxLength(60, ErrorMessage = "Tamanho máximo de 60 caracteres !")]
    public string Email { get; set; }
    
    [PasswordPropertyText]
    public string Password { get; set; }
    
    public string Avatar { get; set; }
    
    public List<Despesa> Despesas { get; set; } = new List<Despesa>();
    
    public List<Card> Cards { get; set; } = new List<Card>();
    
}