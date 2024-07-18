namespace CleanArchitecture.Infrastructure.Data
{
    using System.Data;
    using Dapper;

    // esto es porque DateOnly no e sn tipo nativo en PostgreSQL
    internal sealed class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override DateOnly Parse(object value) => DateOnly.FromDateTime((DateTime)value);

        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.DbType = DbType.Date;
            parameter.Value = value;
        }
    }
}
