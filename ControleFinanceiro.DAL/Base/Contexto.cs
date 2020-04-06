using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.DAL{
    public class Contexto: DbContext
    {
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Ciclo> Ciclos { get; set; }
        public DbSet<Ganho> Ganhos { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<ConfigCiclos> ConfigCiclos { get; set; }
       
        public Contexto(DbContextOptions<Contexto> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigCiclos>()
                .HasOne(c => c.Plano)
                .WithMany(p => p.ConfigCiclos)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConfigCiclos>()
                .HasOne(c => c.Ciclo)
                .WithMany(p => p.ConfigsCiclos)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}