namespace CleanArchitecture.Domain.Alquileres
{
    using CleanArchitecture.Domain.Shared;
    using CleanArchitecture.Domain.Vehiculos;

    public class PrecioService
    {
        public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo)
        {
            var tipoMoneda = vehiculo.Precio!.tipoMoneda;

            var precioPorPeriodo = new Moneda(periodo.CantidadDias * vehiculo.Precio.monto, tipoMoneda);

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

            var cargoPorAccesorios = Moneda.Zero(tipoMoneda);

            if (porcentajeDeCambio > 0)
            {
                cargoPorAccesorios = new Moneda(precioPorPeriodo.monto * porcentajeDeCambio, tipoMoneda);
            }

            var precioTotal = Moneda.Zero();

            precioTotal += precioPorPeriodo;

            if (!vehiculo.PrecioMantenimiento!.IsZero())
            {
                precioTotal += vehiculo.PrecioMantenimiento;
            }

            precioTotal += cargoPorAccesorios;

            return new PrecioDetalle(
                precioPorPeriodo,
                vehiculo.PrecioMantenimiento!,
                cargoPorAccesorios,
                precioTotal);
        }
    }
}
