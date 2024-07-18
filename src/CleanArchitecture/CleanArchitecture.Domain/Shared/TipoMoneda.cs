namespace CleanArchitecture.Domain.Shared;

public record TipoMoneda
{

    public static readonly TipoMoneda None = new (string.Empty);
    public static readonly TipoMoneda Usd = new ("USD");
    public static readonly TipoMoneda Eur = new ("EUR");

    private TipoMoneda(string codigo) => this.Codigo = codigo;

    public string? Codigo { get; init; }

    public static readonly IReadOnlyCollection<TipoMoneda> All =
    [
        Usd,
        Eur
    ];

    public static TipoMoneda FromCodigo(string codigo)
    {
        return All.FirstOrDefault(c => c.Codigo == codigo) ??
            throw new ApplicationException("El tipo de moneda es invalido");
    }
}