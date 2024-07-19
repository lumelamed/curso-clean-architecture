namespace CleanArchitecture.Api.Extensions
{
    using Bogus;
    using CleanArchitecture.Application.Abstractions.Data;
    using CleanArchitecture.Domain.Vehiculos;
    using Dapper;

    public static class SeedDataExtendions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

            using var connection = sqlConnectionFactory.CreateConnection();

            var faker = new Faker();

            var vehiculos = new List<object>();

            for (int i = 0; i < 100; i++)
            {
                vehiculos.Add(new
                {
                    Id = Guid.NewGuid(),
                    Vin = faker.Vehicle.Vin(),
                    Modelo = faker.Vehicle.Model(),
                    Pais = faker.Address.Country(),
                    Departamento = faker.Address.State(),
                    Provincia = faker.Address.County(),
                    Ciudad = faker.Address.City(),
                    Calle = faker.Address.StreetAddress(),
                    PrecioMonto = faker.Random.Decimal(1000, 20000),
                    PrecioTipoMoneda = "USD",
                    PrecioMantenimientoMonto = faker.Random.Decimal(100, 200),
                    PrecioMantenimientoTipoMoneda = "USD",
                    Accesorios = new List<int>
                    {
                        (int)Accesorio.Wifi,
                        (int)Accesorio.AppleCar,
                    },
                    FechaUltima = DateTime.MinValue,
                });
            }

            const string sql = """
                INSERT INTO public.vehiculos
                    (id, vin, modelo, direccion_pais, direccion_departamento, direccion_provincia, direccion_ciudad, direccion_calle, precio_monto, precio_tipo_moneda, precio_mantenimiento_monto, precio_mantenimiento_tipo_moneda, accesorios, fecha_ultima_alquiler)
                    values (@Id, @Vin, @Modelo, @Pais, @Departamento, @Provincia, @Ciudad, @Calle, @PrecioMonto, @PrecioTipoMoneda, @PrecioMantenimientoMonto, @PrecioMantenimientoTipoMoneda, @Accesorios, @FechaUltima)
                """;

            connection.Execute(sql, vehiculos);
        }
    }
}
