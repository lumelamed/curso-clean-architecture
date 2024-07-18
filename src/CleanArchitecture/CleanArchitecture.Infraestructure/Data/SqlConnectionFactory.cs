namespace CleanArchitecture.Infrastructure.Data
{
    using System.Data;
    using CleanArchitecture.Application.Abstractions.Data;
    using Npgsql;

    internal sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string connecitonString;

        public SqlConnectionFactory(string connecitonString)
        {
            this.connecitonString = connecitonString;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new NpgsqlConnection(this.connecitonString);

            connection.Open();

            return connection;
        }
    }
}
