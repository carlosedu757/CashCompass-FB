using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Repositories;

public class CardRepository : DbContext
{
    public CardRepository(DbContextOptions<CardRepository> options) : base(options)
    {
        
    }

    public DbSet<Card> Cards { get; set; }
}