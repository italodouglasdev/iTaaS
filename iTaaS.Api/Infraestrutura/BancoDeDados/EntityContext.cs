using iTaaS.Api.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace iTaaS.Api.Infraestrutura.BancoDeDados
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options) { }


        public DbSet<LogEntidade> Logs { get; set; }
        public DbSet<LogLinhaEntidade> LogsLinhas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogLinhaEntidade>()
                .HasOne(l => l.Log)
                .WithMany(l => l.Linhas)
                .HasForeignKey(l => l.LogId);
        }


    }
}
