using RestAPI.Models.DTO.Request;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models;

public class Receita
{
    public Receita() { }
    public Receita(ReceitaRequestDTO request)
    {
        Value = request.Value;
        Description = request.Description;
        Fornecedor = request.Fornecedor;
    }

    [Key]
    public long ReceitaId { get; set; }
    
    [DisplayFormat(DataFormatString = "C", ApplyFormatInEditMode = true)]
    public decimal Value { get; set; }
    
    [MaxLength(100, ErrorMessage = "O tamanho máximo é de 100 caracteres !")]
    [MinLength(5, ErrorMessage = "O tamanho mínimo é de 5 caracteres !")]
    public string Description { get; set; }

    [MaxLength(50, ErrorMessage = "O tamanho máximo é de 50 caracteres !")]
    [MinLength(5, ErrorMessage = "O tamanho mínimo é de 5 caracteres !")]
    public string Fornecedor { get; set; }
}