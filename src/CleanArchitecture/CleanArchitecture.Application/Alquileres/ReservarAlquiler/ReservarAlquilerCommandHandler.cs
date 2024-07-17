namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler
{
    using System.Threading;
    using System.Threading.Tasks;
    using CleanArchitecture.Application.Abstractions.Clock;
    using CleanArchitecture.Application.Abstractions.Messaging;
    using CleanArchitecture.Domain.Abstractions;
    using CleanArchitecture.Domain.Alquileres;
    using CleanArchitecture.Domain.Users;
    using CleanArchitecture.Domain.Vehiculos;

    internal sealed class ReservarAlquilerCommandHandler : ICommandHandler<ReservarAlquilerCommand, Guid>
    {
        private readonly IUserRepository userRepository;

        private readonly IVehiculoRepository vehiculoRepository;

        private readonly IAlquilerRepository alquilerRepository;

        private readonly PrecioService precioService;

        private readonly IUnitOfWork unitOfWork;

        private readonly IDateTimeProvider dateTimeProvider;

        public ReservarAlquilerCommandHandler(
            IUserRepository userRepository,
            IVehiculoRepository vehiculoRepository,
            IAlquilerRepository alquilerRepository,
            PrecioService precioService,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            this.userRepository = userRepository;
            this.vehiculoRepository = vehiculoRepository;
            this.alquilerRepository = alquilerRepository;
            this.precioService = precioService;
            this.unitOfWork = unitOfWork;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<Guid>> Handle(ReservarAlquilerCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userRepository.GetByIdAsync(request.userId, cancellationToken);

            if (user is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }

            var vehiculo = await this.vehiculoRepository.GetByIdAsync(request.vehiculoId, cancellationToken);

            if (vehiculo is null)
            {
                return Result.Failure<Guid>(VehiculoErrors.NotFound);
            }

            var duracion = DateRange.Create(request.fechaInicio, request.fechaFin);

            if (await this.alquilerRepository.IsOverlappingAsync(vehiculo, duracion, cancellationToken))
            {
                return Result.Failure<Guid>(AlquilerErrors.Overlap);
            }

            var alquiler = Alquiler.Reservar(vehiculo, user.Id, duracion, this.dateTimeProvider.CurrentTime, this.precioService);

            this.alquilerRepository.Add(alquiler);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            return alquiler.Id;
        }
    }
}
