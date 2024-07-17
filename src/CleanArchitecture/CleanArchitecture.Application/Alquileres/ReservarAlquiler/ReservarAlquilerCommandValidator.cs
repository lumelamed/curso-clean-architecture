namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler
{
    using FluentValidation;

    public class ReservarAlquilerCommandValidator : AbstractValidator<ReservarAlquilerCommand>
    {
        public ReservarAlquilerCommandValidator()
        {
            this.RuleFor(c => c.userId).NotEmpty();

            this.RuleFor(c => c.vehiculoId).NotEmpty();

            this.RuleFor(c => c.fechaInicio).LessThan(c => c.fechaFin);
        }
    }
}
