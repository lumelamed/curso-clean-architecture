namespace CleanArchitecture.Domain.Alquileres.Events
{
    using CleanArchitecture.Domain.Abstractions;

    public sealed record AlquilerCanceladoDomainEvent(Guid alquilerId) : IDomainEvent;
}
