using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.DAL{
    public class Contexto: DbContext
    {
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Ciclo> Ciclos { get; set; }
        public DbSet<Ganho> Ganhos { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
       
        public Contexto(DbContextOptions<Contexto> options): base(options)
        {
            
        }

    }
}