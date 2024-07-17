namespace CleanArchitecture.Domain.Reviews
{
    using CleanArchitecture.Domain.Abstractions;

    public static class ReviewErrors
    {
        private static Error notEligible = new ("Review.NotEligible", "Este review y calificacion para el auto no es elegible porque aun no se completa el alquiler");

        public static Error NotEligible
        {
            get { return notEligible; }
        }
    }
}
