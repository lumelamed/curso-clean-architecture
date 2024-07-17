namespace CleanArchitecture.Domain.Reviews
{
    using CleanArchitecture.Domain.Abstractions;

    public sealed record Rating
    {
        private static Error invalid = new ("Rating.Invalid", "El rating es invalido");

        public static Error Invalid
        {
            get { return invalid; }
        }

        public int Value { get; init; }

        // Constructor
        private Rating(int value) => this.Value = value;

        public static Result<Rating> Create(int value)
        {
            if (value < 1 || value > 5)
            {
                return Result.Failure<Rating>(invalid);
            }

            return new Rating(value);
        }
    }
}
