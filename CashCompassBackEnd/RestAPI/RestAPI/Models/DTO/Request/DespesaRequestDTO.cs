using RestAPI.Models.Enum;

namespace RestAPI.Models.DTO;

public class DespesaRequestDTO
{
    
    public decimal Value { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;

    public string Description { get; set; }
    
    public EFormaPagamento FormaPagamento { get; set; }

    public int CardId { get; set; }
    
    public virtual Card Card { get; set; }
    
    public bool WasPaid { get; set; }
}