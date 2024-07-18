﻿namespace CleanArchitecture.Infrastructure
{
    using CleanArchitecture.Application.Abstractions.Clock;
    using CleanArchitecture.Application.Abstractions.Data;
    using CleanArchitecture.Application.Abstractions.Email;
    using CleanArchitecture.Domain.Abstractions;
    using CleanArchitecture.Domain.Alquileres;
    using CleanArchitecture.Domain.Users;
    using CleanArchitecture.Domain.Vehiculos;
    using CleanArchitecture.Infrastructure.Clock;
    using CleanArchitecture.Infrastructure.Data;
    using CleanArchitecture.Infrastructure.Email;
    using CleanArchitecture.Infrastructure.Repositories;
    using Dapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            services.AddTransient<IEmailService, EmailService>();

            var connectionString = configuration.GetConnectionString("Database")
                ?? throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

            // registro los repositorios
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IVehiculoRepository, VehiculoRepository>();

            services.AddScoped<IAlquilerRepository, AlquilerRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            // sql connection
            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            return services;
        }
    }
}
