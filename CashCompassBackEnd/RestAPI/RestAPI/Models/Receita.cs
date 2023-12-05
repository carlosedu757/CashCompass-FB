using RestAPI.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models;

public class Receita
{
    [Key]
    public long ReceitaId { get; set; }
    
    public decimal? Value { get; set; }
    
    public string? Description { get; set; }

    public string? Fornecedor { get; set; }
 
    public DateTime? Date { get; set; }

    public int? CategoriaId { get; set; }

    public FormaPagamento? FormaPagamento { get; set; }

    public int? CardId { get; set; }
}