using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestAPI.Models.DTO.Request;
using RestAPI.Models.Enum;

namespace RestAPI.Models;

public class Despesa
{
    public Despesa(DespesaRequestDTO request)
    {
        Value = request.Value;
        Date = request.Date;
        Description = request.Description;
        WasPaid = request.WasPaid;
    }
    
    [Key]
    public int DespesaId { get; set; }

    [Required]
    public decimal Value { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public string Description { get; set; }

    [Required]
    [ForeignKey("CategoriaId")]
    public Categoria Categoria { get; set; }

    [Required]
    public EFormaPagamento FormaPagamento { get; set; }

    public int CardId { get; set; }

    [ForeignKey("CardId")]
    public virtual Card Card { get; set; }

    [Required]
    public bool WasPaid { get; set; }
}
