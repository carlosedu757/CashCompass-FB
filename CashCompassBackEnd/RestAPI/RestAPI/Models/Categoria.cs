using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models;

public class Categoria
{
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
}
