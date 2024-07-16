namespace CleanArchitecture.Domain.Alquileres.Events
{
    using CleanArchitecture.Domain.Abstractions;

    public sealed record AlquilerRechazadoDomainEvent(Guid alquilerId) : IDomainEvent;
}
