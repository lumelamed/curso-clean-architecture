namespace CleanArchitecture.Domain.Vehiculos
{
    using CleanArchitecture.Domain.Abstractions;
    using CleanArchitecture.Domain.Shared;

    public sealed class Vehiculo : Entity
    {
        public Vehiculo(
            Guid id,
            string modelo,
            string vin,
            Moneda precio,
            Moneda precioMantenimiento,
            DateTime? fechaUltimaAlquiler,
            List<Accesorio> accesorios,
            Direccion? direccion)
            : base(id)
        {
            this.Modelo = modelo;
            this.Vin = vin;
            this.Precio = precio;
            this.PrecioMantenimiento = precioMantenimiento;
            this.FechaUltimaAlquiler = fechaUltimaAlquiler;
            this.Accesorios = accesorios;
            this.Direccion = direccion;
        }

        public string? Modelo { get; private set; }

        public string? Vin { get; private set; }

        public Direccion? Direccion { get; private set; }

        public Moneda? Precio { get; init; }

        public Moneda? PrecioMantenimiento { get; init; }

        public DateTime? FechaUltimaAlquiler { get; internal set; }

        public List<Accesorio> Accesorios { get; private set; } = new ();
    }
}