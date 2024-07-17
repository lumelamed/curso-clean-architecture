namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    using CleanArchitecture.Application.Abstractions.Messaging;

    public record SearchVehiculosQuery(DateOnly fechaInicio, DateOnly fechaFin) : IQuery<IReadOnlyList<VehiculoResponse>>;
}
