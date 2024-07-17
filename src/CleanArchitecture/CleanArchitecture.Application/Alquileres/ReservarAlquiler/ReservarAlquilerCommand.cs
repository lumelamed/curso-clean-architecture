namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler
{
    using CleanArchitecture.Application.Abstractions.Messaging;

    public record ReservarAlquilerCommand(Guid vehiculoId, Guid userId, DateOnly fechaInicio, DateOnly fechaFin) : ICommand<Guid>;
}
