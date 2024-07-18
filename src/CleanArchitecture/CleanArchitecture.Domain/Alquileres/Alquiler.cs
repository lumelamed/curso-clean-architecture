namespace CleanArchitecture.Domain.Alquileres
{
    using System;
    using CleanArchitecture.Domain.Abstractions;
    using CleanArchitecture.Domain.Alquileres.Events;
    using CleanArchitecture.Domain.Shared;
    using CleanArchitecture.Domain.Vehiculos;

    public sealed class Alquiler : Entity
    {
        private Alquiler(
            Guid id,
            Guid vehiculoId,
            Guid userId,
            DateRange duracion,
            Moneda precioPorPerido,
            Moneda precioMantenimiento,
            Moneda precioAccesorios,
            Moneda precioTotal,
            AlquilerStatus status,
            DateTime fechaCreacion)
            : base(id)
        {
            this.Id = id;
            this.VehiculoId = vehiculoId;
            this.UserId = userId;
            this.Duracion = duracion;
            this.PrecioPorPeriodo = precioPorPerido;
            this.PrecioMantenimiento = precioMantenimiento;
            this.PrecioAccesorios = precioAccesorios;
            this.PrecioTotal = precioTotal;
            this.Status = status;
            this.FechaCreacion = fechaCreacion;
        }

        public Guid VehiculoId { get; private set; }

        public Guid UserId { get; private set; }

        public Moneda? PrecioPorPeriodo { get; init; }

        public Moneda? PrecioMantenimiento { get; init; }

        public Moneda? PrecioAccesorios { get; init; }

        public Moneda? PrecioTotal { get; init; }

        public AlquilerStatus Status { get; private set; }

        public DateRange? Duracion { get; private set; }

        public DateTime? FechaCreacion { get; private set; }

        public DateTime? FechaConfirmacion { get; private set; }

        public DateTime? FechaDenegacion { get; private set; }

        public DateTime? FechaCompletado { get; private set; }

        public DateTime? FechaCancelacion { get; private set; }

        public static Alquiler Reservar(Vehiculo vehiculo, Guid userId, DateRange duracion, DateTime fechaCreacion, PrecioService precioService)
        {
            var precioDetalle = precioService.CalcularPrecio(vehiculo, duracion);

            var alquiler = new Alquiler(
                    Guid.NewGuid(),
                    vehiculo.Id,
                    userId,
                    duracion,
                    precioDetalle.precioPeriodo,
                    precioDetalle.precioMantenimiento,
                    precioDetalle.precioAccesorios,
                    precioDetalle.precioTotal,
                    AlquilerStatus.Reservado,
                    fechaCreacion);

            alquiler.RaiseDomainEvents(new AlquilerReservadoDomainEvent(alquiler.Id!));

            vehiculo.FechaUltimaAlquiler = fechaCreacion;

            return alquiler;
        }

        public Result Confirmar(DateTime utcNow)
        {
            if (this.Status != AlquilerStatus.Reservado)
            {
                return Result.Failure(AlquilerErrors.NoReservado);
            }

            this.Status = AlquilerStatus.Confirmado;
            this.FechaConfirmacion = utcNow;

            this.RaiseDomainEvents(new AlquilerConfirmadoDomainEvent(this.Id));

            return Result.Success();
        }

        public Result Rechazar(DateTime utcNow)
        {
            if (this.Status != AlquilerStatus.Reservado)
            {
                return Result.Failure(AlquilerErrors.NoReservado);
            }

            this.Status = AlquilerStatus.Rechazado;
            this.FechaConfirmacion = utcNow;

            this.RaiseDomainEvents(new AlquilerRechazadoDomainEvent(this.Id));

            return Result.Success();
        }

        public Result Cancelar(DateTime utcNow)
        {
            if (this.Status != AlquilerStatus.Confirmado)
            {
                return Result.Failure(AlquilerErrors.NoConfirmado);
            }

            var fecha = DateOnly.FromDateTime(utcNow);

            if (fecha > this.Duracion!.Inicio)
            {
                return Result.Failure(AlquilerErrors.YaEmpezo);
            }

            this.Status = AlquilerStatus.Cancelado;
            this.FechaConfirmacion = utcNow;

            this.RaiseDomainEvents(new AlquilerCanceladoDomainEvent(this.Id));

            return Result.Success();
        }

        public Result Completar(DateTime utcNow)
        {
            if (this.Status != AlquilerStatus.Confirmado)
            {
                return Result.Failure(AlquilerErrors.NoConfirmado);
            }

            this.Status = AlquilerStatus.Completado;
            this.FechaConfirmacion = utcNow;

            this.RaiseDomainEvents(new AlquilerCompletadoDomainEvent(this.Id));

            return Result.Success();
        }
    }
}
