namespace CleaArchitecture.Domain.Abstractions
{
    using CleanArchitecture.Domain.Abstractions;

    public abstract class Entity
    {
        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();

        protected Entity(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; init; }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return this.domainEvents.ToList();
        }

        public void ClearDomainEvents()
        {
            this.domainEvents.Clear();
        }

        public void RaiseDomainEvents(IDomainEvent domainEvent)
        {
            this.domainEvents.Add(domainEvent);
        }
    }
}