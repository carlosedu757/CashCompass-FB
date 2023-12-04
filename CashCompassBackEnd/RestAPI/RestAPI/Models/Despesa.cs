using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestAPI.Models.Enum;

namespace RestAPI.Models;

public class Despesa
{
    [Key]
    public int DespesaId { get; set; }

    [Required]
    public decimal Value { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public string Description { get; set; }

    public int CategoriaId { get; set; }

    [Required]
    [ForeignKey("CategoriaId")]
    public Categoria Categoria { get; set; }

    [Required]
    public FormaPagamento FormaPagamento { get; set; }

    //public int CardId { get; set; }

    //[ForeignKey("CardId")]
    //public virtual Card Card { get; set; }

    [Required]
    public bool WasPaid { get; set; }
}
