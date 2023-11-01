using RestAPI.Models.Enum;

namespace RestAPI.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ECategory Category { get; set; }
        public EFormaPagamento FormaPagamento { get; set; }
        public bool IsOpen { get; set; }
    }
}
