using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Repositories;

public class ReceitaRepository : DbContext
{
    public ReceitaRepository(DbContextOptions<ReceitaRepository> options ) : base(options)
    {}

    public DbSet<Receita> Receitas { get; set; }
}