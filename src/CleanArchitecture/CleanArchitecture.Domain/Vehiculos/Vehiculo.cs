namespace CleanArchitecture.Domain.Vehiculos
{
    using CleaArchitecture.Domain.Abstractions;
    using CleaArchitecture.Domain.Vehiculos;

    public sealed class Vehiculo : Entity
    {
        public Vehiculo(Guid id)
            : base(id)
        {
        }

        public string? Modelo { get; private set; }

        public string? Vin { get; private set; }

        public Direccion? Direccion { get; private set; }

        public decimal? Precio { get; private set; }

        public DateTime? FechaUltimaAlquiler { get; private set; }

        public List<Accesorio> Accesorios { get; private set; } = new ();
    }
}