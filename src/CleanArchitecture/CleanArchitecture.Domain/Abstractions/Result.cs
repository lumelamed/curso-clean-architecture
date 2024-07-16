namespace CleanArchitecture.Domain.Abstractions
{
    using System.Diagnostics.CodeAnalysis;

    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            this.IsSuccess = isSuccess;
            this.Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !this.IsSuccess;

        public Error Error { get; }

        public static Result Success() => new (true, Error.None);

        public static Result Failure(Error error) => new (false, error);

        public static Result<TValue> Success<TValue>(TValue value) => new (value, true, Error.None);

        public static Result<TValue> Failure<TValue>(Error error) => new (default, false, error);

        public static Result<TValue> Create<TValue>(TValue value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }

    public class Result<TValue> : Result
    {
        protected internal Result(TValue? value, bool isSucces, Error error)
            : base(isSucces, error)
        {
            this.value = value;
        }

        [NotNull]
        public TValue Value => this.IsSuccess ? this.value! : throw new InvalidOperationException("El resultado del valor de error no es adminisble");

        private TValue? value { get; set; }

        public static implicit operator Result<TValue>(TValue value) => Create(value);
    }
}
