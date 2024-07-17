namespace CleanArchitecture.Application
{
    using CleanArchitecture.Domain.Alquileres;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddTransient<PrecioService>();

            return services;
        }
    }
}
