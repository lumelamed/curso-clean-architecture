namespace CleanArchitecture.Domain.Alquileres
{
    using CleaArchitecture.Domain.Shared;

    public record PrecioDetalle (
        Moneda precioPeriodo,
        Moneda precioMantenimiento,
        Moneda precioAccesorios,
        Moneda precioTotal
    );
}
