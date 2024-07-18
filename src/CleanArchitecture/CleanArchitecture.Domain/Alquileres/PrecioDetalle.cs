namespace CleanArchitecture.Domain.Alquileres
{
    using CleanArchitecture.Domain.Shared;

    public record PrecioDetalle (
        Moneda precioPeriodo,
        Moneda precioMantenimiento,
        Moneda precioAccesorios,
        Moneda precioTotal
    );
}
