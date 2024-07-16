namespace CleanArchitecture.Domain.Abstractions
{
    public record Error(string code, string description)
    {
        private static Error none = new (string.Empty, string.Empty);

        private static Error nullValue = new ("Error.NullValue", "Un valor null fue ingresado");

        public static Error None
        {
            get { return none; }
        }

        public static Error NullValue
        {
            get { return nullValue; }
        }
    }
}
