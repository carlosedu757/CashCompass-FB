using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models;

public class Receitas
{
    [Key]
    public long Id { get; set; }
    
    [DisplayFormat(DataFormatString = "C", ApplyFormatInEditMode = true)]
    public decimal Value { get; set; }
    
    [MaxLength(50, ErrorMessage = "O tamanho máximo é de 50 caracteres !")]
    [MinLength(5, ErrorMessage = "O tamanho mínimo é de 5 caracteres !")]
    public string Description { get; set; }

    [MaxLength(50, ErrorMessage = "O tamanho máximo é de 50 caracteres !")]
    [MinLength(5, ErrorMessage = "O tamanho mínimo é de 5 caracteres !")]
    public string Fornecedor { get; set; }
}