﻿namespace CleanArchitecture.Domain.Users
{
    using CleanArchitecture.Domain.Alquileres;
    using CleanArchitecture.Domain.Vehiculos;

    public interface IAlquilerRepository
    {
        Task<Alquiler?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> IsOverlappingAsync(Vehiculo vehiculo, DateRange duracion, CancellationToken cancellationToken = default);

        void Add(Alquiler alquiler);
    }
}
