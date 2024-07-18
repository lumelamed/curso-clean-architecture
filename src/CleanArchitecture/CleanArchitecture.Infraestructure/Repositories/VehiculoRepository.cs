namespace CleanArchitecture.Infrastructure.Repositories
{
    using CleanArchitecture.Domain.Vehiculos;

    internal sealed class VehiculoRepository : Repository<Vehiculo>, IVehiculoRepository
    {
        public VehiculoRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
