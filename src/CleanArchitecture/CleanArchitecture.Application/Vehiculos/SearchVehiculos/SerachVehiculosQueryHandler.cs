﻿namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CleanArchitecture.Application.Abstractions.Data;
    using CleanArchitecture.Application.Abstractions.Messaging;
    using CleanArchitecture.Domain.Abstractions;
    using CleanArchitecture.Domain.Alquileres;
    using Dapper;

    internal sealed class SerachVehiculosQueryHandler : IQueryHandler<SearchVehiculosQuery, IReadOnlyList<VehiculoResponse>>
    {
        private static readonly int[] ActiveAlquilerStatuses =
        {
            (int)AlquilerStatus.Reservado,
            (int)AlquilerStatus.Confirmado,
            (int)AlquilerStatus.Completado,
        };

        private readonly ISqlConnectionFactory connectionFactory;

        public SerachVehiculosQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<Result<IReadOnlyList<VehiculoResponse>>> Handle(SearchVehiculosQuery request, CancellationToken cancellationToken)
        {
            if (request.fechaInicio > request.fechaFin)
            {
                return new List<VehiculoResponse>();
            }

            using var connection = this.connectionFactory.CreateConnection();

            const string sql = """
                SELECT
                    a.id as Id,
                    a.modelo as Modelo,
                    a.vin as Vin,
                    a.precio_monto as Precio,
                    a.precio_tipo_modesa as TipoMoneda,
                    a.direccion_pais as Pais,
                    a.direccion_departamento as Departamento,
                    a.direccion_provincia as Provincia,
                    a.direccion_ciudad as Ciudad,
                    a.direccion_calle as Calle,
                FROM vehiculos AS a
                WHERE NOT EXIST
                (
                    SELECT 1
                    FROM alquileres AS b
                    WHERE
                        b.vehiculo_id = a.id AND
                        b.duracion_inicio <= @EndDate AND
                        b.duracion_final >= @StartDate AND
                        b.status = ANY(@ActiveAlquilerStatuses)

                """;

            var vehiculos = await connection.QueryAsync<VehiculoResponse, DireccionResponse, VehiculoResponse>(
                    sql,
                    (vehiculo, direccion) =>
                    {
                        vehiculo.Direccion = direccion;
                        return vehiculo;
                    }, new
                    {
                        StartDate = request.fechaInicio,
                        EndDate = request.fechaFin,
                        ActiveAlquilerStatuses,
                    },
                    splitOn: "Pais");

            return vehiculos.ToList();
        }
    }
}