namespace CleanArchitecture.Domain.Shared;

public record Moneda(decimal monto, TipoMoneda tipoMoneda)
{

    public static Moneda operator +(Moneda primero, Moneda segundo)
    {
        if (primero.tipoMoneda != segundo.tipoMoneda)
        {
            throw new InvalidOperationException("El tipo de moneda debe ser el mismo");
        }

        return new Moneda(primero.monto + segundo.monto, primero.tipoMoneda);
    }

    public static Moneda Zero() => new (0, TipoMoneda.None);
    public static Moneda Zero(TipoMoneda tipoMoneda) => new (0, tipoMoneda);

    public bool IsZero() => this == Zero(this.tipoMoneda);

}