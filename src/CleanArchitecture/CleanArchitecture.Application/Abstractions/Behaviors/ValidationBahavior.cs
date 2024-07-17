namespace CleanArchitecture.Application.Abstractions.Behaviors
{
    using System.Threading;
    using System.Threading.Tasks;
    using CleanArchitecture.Application.Abstractions.Messaging;
    using CleanArchitecture.Application.Exceptions;
    using FluentValidation;
    using MediatR;

    public class ValidationBahavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBahavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!this.validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var validationErrors = this.validators
                .Select(validators => validators.Validate(context))
                .Where(validationResult => validationResult.Errors.Any())
                .SelectMany(validationResult => validationResult.Errors)
                .Select(validationFailure => new ValidationError(validationFailure.PropertyName, validationFailure.ErrorMessage)) // proyeccion, transformo de una clase a otro
                .ToList();

            if (validationErrors.Any())
            {
                throw new Exceptions.ValidationException(validationErrors);
            }

            return await next();
        }
    }
}
