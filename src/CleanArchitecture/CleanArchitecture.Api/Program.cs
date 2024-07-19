namespace CleanArchitecture.Api
{
    using CleanArchitecture.Api.Extensions;
    using CleanArchitecture.Application;
    using CleanArchitecture.Infrastructure;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // lo agrgue desde ApplicationBuilderExtensions
            app.ApplyMigration();

            // no usaban esta linea en el ejemplo
            // app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}