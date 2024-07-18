namespace CleanArchitecture.Infrastructure.Repositories
{
    using CleanArchitecture.Domain.Users;

    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
