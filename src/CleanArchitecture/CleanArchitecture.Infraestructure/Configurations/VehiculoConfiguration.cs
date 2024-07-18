namespace CleanArchitecture.Infrastructure.Configurations
{
    using CleaArchitecture.Domain.Shared;
    using CleanArchitecture.Domain.Vehiculos;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
    {
        public void Configure(EntityTypeBuilder<Vehiculo> builder)
        {
            builder.ToTable("vehiculos");  // nombre de la tabla

            builder.HasKey(vehiculo => vehiculo.Id); // PK

            builder.OwnsOne(vehiculo => vehiculo.Direccion); // a nivel de PosgreSQL, se agrgean esos campos a la tabla vehiculos

            builder.Property(vehiculo => vehiculo.Modelo).HasMaxLength(200); // tiene que ser un tipo primitivo y no un Object Value, si lo fuera, hay que hacer una conversion con HasConversion()

            builder.Property(vehiculo => vehiculo.Vin).HasMaxLength(200);

            builder.OwnsOne(vehiculo => vehiculo.Precio, priceBuilder =>
            {
                priceBuilder.Property(moneda => moneda.tipoMoneda)
                .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
            });

            builder.OwnsOne(vehiculo => vehiculo.PrecioMantenimiento, priceBuilder =>
            {
                priceBuilder.Property(moneda => moneda.tipoMoneda)
                .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
            });

            // optimist concurrency: cada cambio crea una "version" y bloquea el registro para que no haya problemas de ocncurrencia si alguien mas lo intenta modificar
            builder.Property<uint>("Version").IsRowVersion();
        }
    }
}
