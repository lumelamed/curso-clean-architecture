namespace CleanArchitecture.Domain.Reviews.Events
{
    using CleanArchitecture.Domain.Abstractions;

    public sealed record ReviewCreatedDomainEvent(Guid reviewId) : IDomainEvent;
}
