namespace CleanArchitecture.Domain.Reviews
{
    using System;
    using CleanArchitecture.Domain.Abstractions;
    using CleanArchitecture.Domain.Alquileres;
    using CleanArchitecture.Domain.Reviews.Events;

    public sealed class Review : Entity
    {
        private Review()
        {
        }

        private Review(
            Guid id,
            Guid vehiculoId,
            Guid alquilerId,
            Guid userId,
            Rating rating,
            Comentario comentario,
            DateTime? fechaCreacion)
            : base(id)
        {
            this.AlquilerId = alquilerId;
            this.VehiculoId = vehiculoId;
            this.UserId = userId;
            this.Rating = rating;
            this.Comentario = comentario;
            this.FechaCreacion = fechaCreacion;
        }

        public Guid VehiculoId { get; private set; }

        public Guid AlquilerId { get; private set; }

        public Guid UserId { get; private set; }

        public Rating Rating { get; private set; }

        public Comentario? Comentario { get; private set; }

        public DateTime? FechaCreacion { get; private set; }

        public static Result<Review> Create(Alquiler alquiler, Rating rating, Comentario comentario, DateTime fechaCreacion)
        {
            if (alquiler.Status != AlquilerStatus.Completado)
            {
                return Result.Failure<Review>(ReviewErrors.NotEligible);
            }

            var review = new Review(Guid.NewGuid(), alquiler.VehiculoId, alquiler.Id, alquiler.UserId, rating, comentario, fechaCreacion);

            review.RaiseDomainEvents(new ReviewCreatedDomainEvent(review.Id));
            return review;
        }
    }
}
