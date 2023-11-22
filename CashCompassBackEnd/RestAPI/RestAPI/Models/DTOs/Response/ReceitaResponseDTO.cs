namespace RestAPI.Models.DTO.Response;

public class ReceitaResponseDTO
{
    public int Id { get; set; }
    
    public decimal Value { get; set; }
    
    public string Description { get; set; }
    
    public string Fornecedor { get; set; }
}