namespace CleanArchitecture.Domain.Users.Events
{
    using CleanArchitecture.Domain.Abstractions;

    public sealed record UserCreatedDomainEvent(Guid userId) : IDomainEvent
    {
    }
}
