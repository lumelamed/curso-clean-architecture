namespace CleanArchitecture.Application.Alquileres.GetAlquiler
{
    using System.Threading;
    using System.Threading.Tasks;
    using CleanArchitecture.Application.Abstractions.Data;
    using CleanArchitecture.Application.Abstractions.Messaging;
    using CleanArchitecture.Domain.Abstractions;
    using Dapper;

    // no es expuesto en componentes externos, por eso no es publico
    internal sealed class GetAlquilerQueryHandler : IQueryHandler<GetAlquilerQuery, AlquilerResponse>
    {
        private readonly ISqlConnectionFactory connectionFactory;

        public GetAlquilerQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<Result<AlquilerResponse>> Handle(GetAlquilerQuery request, CancellationToken cancellationToken)
        {
            using var connection = this.connectionFactory.CreateConnection();

            // PostreSQL es case sensitive!!
            const string sql = """ 
                SELECT 
                    id as Id
                    vehiculo_id AS VehiculoId,
                    user_id as UserId,
                    status as Status,
                    precio_por_periodo as PrecioAlquiler,
                    precio_por_periodo_tipo_moneda as TipoMonedaAlquiler,
                    precio_mantenimiento as PrecioMantenimiento,
                    precio_mantenimiento_tipo_moneda as TipoMonedaAlquiler,
                    precio_accesorios as PrecioAccesorios,
                    precio_accesorios_tipo_moneda as TipoMonedaPrecioAccesorios,
                    precio_total as PrecioTotal,
                    precio_total_tipo_moneda as TipoMonedaPrecioTotal,
                    duracion_inicio as DuracionInicio,
                    duracion_final as DuracionFinal,
                    fecha_creacion as FechaCreacion
                    FROM alquileres WHERE id = @AlquilerId
            """;

            var alquiler = await connection.QueryFirstOrDefaultAsync<AlquilerResponse>(
                sql, new
                {
                    request.alquilerId,
                });

            return alquiler;
        }
    }
}
