namespace CleanArchitecture.Domain.Vehiculos
{
    using CleanArchitecture.Domain.Abstractions;

    public static class VehiculoErrors
    {
        private static Error notFound = new ("User.Found", "El vehiculo con el Id especificado no fue encontrado");

        public static Error NotFound
        {
            get { return notFound; }
        }
    }
}
