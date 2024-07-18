namespace CleanArchitecture.Infrastructure
{
    using CleanArchitecture.Domain.Abstractions;
    using Microsoft.EntityFrameworkCore;

    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly); // escanea el assembly buscando las configuraciones de cada entidad
            base.OnModelCreating(modelBuilder);
        }
    }
}
