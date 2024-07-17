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
    }
}
