namespace CleanArchitecture.Domain.Alquileres.Events
{
    using CleanArchitecture.Domain.Abstractions;

    public sealed record AlquilerConfirmadoDomainEvent(Guid alquilerId) : IDomainEvent;
}
