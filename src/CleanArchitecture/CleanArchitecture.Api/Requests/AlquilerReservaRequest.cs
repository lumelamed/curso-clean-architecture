namespace CleanArchitecture.Api.Requests
{
    public sealed record AlquilerReservaRequest(
        Guid vehiculoId,
        Guid userId,
        DateOnly startDate,
        DateOnly endDate);
}
