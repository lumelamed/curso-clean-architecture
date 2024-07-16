namespace CleanArchitecture.Domain.Alquileres.Events
{
    using CleanArchitecture.Domain.Abstractions;

    public sealed record AlquilerReservadoDomainEvent(Guid alquilerId) : IDomainEvent;
}
