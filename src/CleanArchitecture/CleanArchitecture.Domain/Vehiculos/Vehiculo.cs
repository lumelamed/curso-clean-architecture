namespace CleanArchitecture.Domain.Vehiculos
{
    using CleanArchitecture.Domain.Abstractions;

    public sealed class Vehiculo : Entity
    {
        public Vehiculo(Guid id)
            : base(id)
        {
        }

        public string? Modelo { get; private set; }

        public string? Vin { get; private set; }

        public Direccion? Direccion { get; private set; }

        public decimal? Precio { get; init; }

        public decimal? PrecioMantenimiento { get; init; }

        public DateTime? FechaUltimaAlquiler { get; internal set; }

        public List<Accesorio> Accesorios { get; private set; } = new ();
    }
}