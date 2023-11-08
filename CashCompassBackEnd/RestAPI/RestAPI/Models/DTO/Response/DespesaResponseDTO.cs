using RestAPI.Models.Enum;

namespace RestAPI.Models.DTO.Response;

public class DespesaResponseDTO
{
    public DespesaResponseDTO(Despesa despesa)
    {
        Id = despesa.Id;
        Value = despesa.Value;
        Date = despesa.Date;
        Description = despesa.Description;
        Category = despesa.Category;
        FormaPagamento = despesa.FormaPagamento;
        IsOpen = despesa.IsOpen;
    }

    public int Id { get; set; }
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public ECategory Category { get; set; }
    public EFormaPagamento FormaPagamento { get; set; }
    public bool IsOpen { get; set; }
}