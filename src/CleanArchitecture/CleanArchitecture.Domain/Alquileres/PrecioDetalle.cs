namespace CleanArchitecture.Domain.Alquileres
{
    public record PrecioDetalle (
        decimal precioPeriodo,
        decimal precioMantenimiento,
        decimal precioAccesorios,
        decimal precioTotal
    );
}
