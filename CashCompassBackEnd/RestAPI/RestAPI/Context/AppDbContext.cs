using RestAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace RestAPI.Context;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Card>? Card { get; set; }

    public DbSet<Receita>? Receita { get; set; }

    public DbSet<Despesa>? Despesa { get; set; }

    public DbSet<User>? User { get; set; }

    public DbSet<Categoria>? Categoria { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => p.UserId);

        modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();

        modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();


        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId); 

            entity.Property(e => e.CardId).ValueGeneratedOnAdd();

            entity.Property(e => e.CardNumber)
                .IsRequired()
                .HasMaxLength(16);

            entity.Property(e => e.LimitValue).HasColumnType("decimal(7, 2)");

            entity.Property(e => e.CurrentValue).HasColumnType("decimal(7, 2)");

            entity.Property(e => e.DateClose).HasColumnType("long");

            entity.Property(e => e.Bandeira).IsRequired();

            entity.Property(e => e.Type).IsRequired();
        });

        modelBuilder.Entity<Receita>(entity =>
        {
            entity.HasKey(e => e.ReceitaId);

            entity.Property(e => e.Value).HasColumnType("decimal(7, 2)").IsRequired();

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Fornecedor)
                .HasMaxLength(50)
                .IsRequired();
        });

        modelBuilder.Entity<Despesa>(entity =>
        {
            entity.HasKey(e => e.DespesaId);

            entity.Property(e => e.Value).HasColumnType("decimal(7, 2)").IsRequired();

            entity.Property(e => e.Date).IsRequired();

            entity.Property(e => e.Description);

            entity.Property(e => e.FormaPagamento).IsRequired();

            entity.Property(e => e.WasPaid).IsRequired();

            entity.HasOne(e => e.Categoria)
                .WithMany()
                .HasForeignKey(e => e.CategoriaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId);

            entity.Property(e => e.Nome).HasMaxLength(60).IsRequired();

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.Name)
                .HasMaxLength(60);

            entity.Property(e => e.Email)
                .HasMaxLength(50);

            entity.Property(e => e.Password);
        });
    }

}
