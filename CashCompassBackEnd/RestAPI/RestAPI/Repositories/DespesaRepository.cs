using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Repositories;

public class DespesaRepository : DbContext
{
    public DespesaRepository(DbContextOptions<DespesaRepository> options) : base(options)
    {
        
    }

    public DbSet<Despesa> Despesas { get; set; }    
}