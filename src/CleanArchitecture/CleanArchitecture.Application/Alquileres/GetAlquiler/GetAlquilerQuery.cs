namespace CleanArchitecture.Application.Alquileres.GetAlquiler
{
    using CleanArchitecture.Application.Abstractions.Messaging;

    public sealed record GetAlquilerQuery(Guid alquilerId) : IQuery<AlquilerResponse>
    {
    }
}
