using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [MinLength(4, ErrorMessage = "O tamanho mínimo é de 4 caracteres")]
        [MaxLength(60, ErrorMessage = "O tamanho máximo é de 60 caracteres")]
        public string Name { get; set; }
        
        [EmailAddress(ErrorMessage = "Email inválido !")]
        public string Email { get; set; }
        
        [PasswordPropertyText]
        public string Password { get; set; }

        [DisplayFormat(DataFormatString = "C")]
        public decimal Saldo { get; set; }
        
        public string Avatar { get; set; }

        public List<Despesa> Despesas { get; set; }
        public List<Card> Cards { get; set; }
        
    }
}
