namespace CleanArchitecture.Domain.Alquileres
{
    using CleanArchitecture.Domain.Vehiculos;

    public class PrecioService
    {
        public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo)
        {
            var precioPorPeriodo = periodo.CantidadDias * vehiculo.Precio;

            decimal porcentajeDeCambio = 0;

            foreach (var accesorio in vehiculo.Accesorios)
            {
                porcentajeDeCambio += accesorio switch
                {
                    Accesorio.AppleCar or Accesorio.AndroidCar => 0.05m,
                    Accesorio.AireAcondicionado => 0.01m,
                    Accesorio.Mapas => 0.01m,
                    _ => 0
                };
            }

            var cargoPorAccesorios = 0m;

            if (porcentajeDeCambio > 0)
            {
                cargoPorAccesorios = (decimal)(precioPorPeriodo! * porcentajeDeCambio);
            }

            var precioTotal = precioPorPeriodo;

            if (vehiculo.PrecioMantenimiento != 0)
            {
                precioTotal += vehiculo.PrecioMantenimiento;
            }

            if (vehiculo.PrecioMantenimiento != 0)
            {
                precioTotal += vehiculo.PrecioMantenimiento;
            }

            precioTotal += cargoPorAccesorios;

            return new PrecioDetalle(
                (decimal)precioPorPeriodo!,
                (decimal)vehiculo.PrecioMantenimiento!,
                cargoPorAccesorios,
                (decimal)precioTotal!);
        }
    }
}
