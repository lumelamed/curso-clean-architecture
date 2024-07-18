namespace CleanArchitecture.Infrastructure
{
    using CleanArchitecture.Application.Exceptions;
    using CleanArchitecture.Domain.Abstractions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IPublisher publisher;

        public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
            : base(options)
        {
            this.publisher = publisher;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                await this.PublishDomainEventsAsync();

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("La excepcion por concurrencia se disparó", ex);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly); // escanea el assembly buscando las configuraciones de cada entidad
            base.OnModelCreating(modelBuilder);
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = this.ChangeTracker
                .Entries<Entity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();
                    entity.ClearDomainEvents();
                    return domainEvents;
                }).ToList();

            foreach (var domainEvent in domainEvents)
            {
                await this.publisher.Publish(domainEvent);
            }
        }
    }
}
