namespace CleanArchitecture.Infrastructure.Clock
{
    using CleanArchitecture.Application.Abstractions.Clock;

    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime CurrentTime => DateTime.UtcNow;
    }
}
