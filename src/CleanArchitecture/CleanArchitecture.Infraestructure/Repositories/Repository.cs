namespace CleanArchitecture.Infrastructure.Repositories
{
    using CleanArchitecture.Domain.Abstractions;
    using Microsoft.EntityFrameworkCore;

    internal abstract class Repository<T>
        where T : Entity
    {
        protected readonly ApplicationDbContext dbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await this.dbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        }

        public void Add(T entity)
        {
            this.dbContext.Add(entity);
        }
    }
}
