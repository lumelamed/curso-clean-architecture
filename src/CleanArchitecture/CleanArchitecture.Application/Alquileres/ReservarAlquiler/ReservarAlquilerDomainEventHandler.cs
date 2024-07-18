namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler
{
    using System.Threading;
    using System.Threading.Tasks;
    using CleanArchitecture.Application.Abstractions.Email;
    using CleanArchitecture.Domain.Alquileres;
    using CleanArchitecture.Domain.Alquileres.Events;
    using CleanArchitecture.Domain.Users;
    using MediatR;

    internal sealed class ReservarAlquilerDomainEventHandler : INotificationHandler<AlquilerReservadoDomainEvent>
    {
        private readonly IAlquilerRepository alquilerRepository;

        private readonly IUserRepository userRepository;

        private readonly IEmailService emailService;

        public ReservarAlquilerDomainEventHandler(
            IAlquilerRepository alquilerRepository,
            IUserRepository userRepository,
            IEmailService emailService)
        {
            this.alquilerRepository = alquilerRepository;
            this.userRepository = userRepository;
            this.emailService = emailService;
        }

        public async Task Handle(AlquilerReservadoDomainEvent notification, CancellationToken cancellationToken)
        {
            var alquiler = await this.alquilerRepository.GetByIdAsync(notification.alquilerId, cancellationToken);

            if (alquiler is null)
            {
                return;
            }

            var user = await this.userRepository.GetByIdAsync(alquiler.UserId, cancellationToken);

            if (user is null)
            {
                return;
            }

            await this.emailService.SendAsync(user.Email!, "Alquiler reservado", "Tenés que confirmar la resrva, sino se va a perder");
        }
    }
}
