using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestAPI.Models.Enum;

namespace RestAPI.Models;

public class Despesa
{
    [Key]
    public int DespesaId { get; set; }

    public decimal? Value { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public int? CategoriaId { get; set; }

    public FormaPagamento? FormaPagamento { get; set; }

    public int? CardId { get; set; }

    public bool? WasPaid { get; set; }

    public int? NumParcelas { get; set; }
}
