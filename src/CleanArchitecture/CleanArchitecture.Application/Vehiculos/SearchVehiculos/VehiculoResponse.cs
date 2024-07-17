﻿namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    public sealed class VehiculoResponse
    {
        public Guid Id { get; init; }

        public string? Modelo { get; init; }

        public string? Descripcion { get; init; }

        public string? Vin { get; init; }

        public decimal Precio { get; init; }

        public decimal TipoMoneda { get; init; }

        public DireccionResponse? Direccion { get; set; }
    }
}