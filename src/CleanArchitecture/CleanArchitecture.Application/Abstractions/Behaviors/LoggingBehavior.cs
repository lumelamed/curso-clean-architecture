namespace CleanArchitecture.Application.Abstractions.Behaviors
{
    using System.Threading;
    using System.Threading.Tasks;
    using CleanArchitecture.Application.Abstractions.Messaging;
    using MediatR;
    using Microsoft.Extensions.Logging;

    // Logging propio de Microsoft en vez de otros, como Serilog
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly ILogger<TRequest> logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse?> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            try
            {
                this.logger.LogInformation($"Ejecutando el command request {name}");

                var result = await next();

                this.logger.LogInformation($"El comando {name} se ejecutó exitosamente");

                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"El comando {name} tuvo errores");
            }

            return default;
        }
    }
}
