﻿using RestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RestAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Card>? Card { get; set; }

    public DbSet<Receita>? Receita { get; set; }

    public DbSet<Despesa>? Despesa { get; set; }

    public DbSet<User>? User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId); // Define a chave primária

            entity.Property(e => e.CardId).ValueGeneratedOnAdd(); // Autoincremento

            entity.Property(e => e.CardNumber)
                .IsRequired()
                .HasMaxLength(16);

            entity.Property(e => e.LimitValue).HasColumnType("decimal(18, 2)").IsRequired();

            entity.Property(e => e.CurrentValue).HasColumnType("decimal(18, 2)").IsRequired();

            entity.Property(e => e.DateClose).HasColumnType("date").IsRequired();

            entity.Property(e => e.Bandeira).IsRequired();

            entity.Property(e => e.Type).IsRequired();

            entity.HasOne(e => e.User) // Relacionamento com User
                .WithMany(u => u.Cards)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false) // Se quiser permitir Cards sem User, use true
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Despesa
            entity.HasMany(e => e.Despesas)
                .WithOne(d => d.Card)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.Cascade); // Se for necessário deletar Despesas vinculadas ao deletar um Card
        });

        modelBuilder.Entity<Receita>(entity =>
        {
            entity.HasKey(e => e.ReceitaId);

            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)").IsRequired();

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

            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)").IsRequired();

            entity.Property(e => e.Date).IsRequired();

            entity.Property(e => e.Description);

            entity.Property(e => e.Category).IsRequired();

            entity.Property(e => e.FormaPagamento).IsRequired();

            entity.Property(e => e.WasPaid).IsRequired();

            entity.HasOne(e => e.Card)
                .WithMany(c => c.Despesas)
                .HasForeignKey(e => e.CardId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.Name)
                .HasMaxLength(60);

            entity.Property(e => e.Email)
                .HasMaxLength(50);

            entity.Property(e => e.Password);

            entity.Property(e => e.Avatar);

            entity.HasMany(e => e.Cards)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Se for necessário deletar Cards vinculados ao deletar um User
        });
    }

}