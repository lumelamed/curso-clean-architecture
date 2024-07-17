namespace CleanArchitecture.Application.Abstractions.Data
{
    using System.Data;

    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
