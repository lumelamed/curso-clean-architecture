namespace CleanArchitecture.Domain.Alquileres.Events
{
    using CleanArchitecture.Domain.Abstractions;

    public sealed record AlquilerCompletadoDomainEvent(Guid alquilerId) : IDomainEvent;
}
