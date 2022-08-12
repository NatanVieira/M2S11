using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using M2S11Pets.Models;

namespace M2S11Pets.Data
{
    public partial class PetsDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public PetsDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PetsDbContext(DbContextOptions<PetsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pessoa> Pessoas { get; set; } = null!;
        public virtual DbSet<Pet> Pets { get; set; } = null!;
        public virtual DbSet<Raca> Racas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CONEXAO_BANCO"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasMany(d => d.Pets)
                    .WithMany(p => p.Pessoas)
                    .UsingEntity<Dictionary<string, object>>(
                        "PessoasPet",
                        l => l.HasOne<Pet>().WithMany().HasForeignKey("PetId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PessoasPet_Pet"),
                        r => r.HasOne<Pessoa>().WithMany().HasForeignKey("PessoaId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PessoasPet_Pessoa"),
                        j =>
                        {
                            j.HasKey("PessoaId", "PetId");

                            j.ToTable("PessoasPet");
                        });
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Raca)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.RacaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pets_Racaid");
            });

            modelBuilder.Entity<Raca>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
