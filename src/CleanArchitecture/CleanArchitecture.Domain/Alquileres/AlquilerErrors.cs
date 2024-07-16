namespace CleanArchitecture.Domain.Alquileres
{
    using CleanArchitecture.Domain.Abstractions;

    public static class AlquilerErrors
    {
        private static Error notFound = new ("Alquiler.Found", "El alquiler con el Id especificado no fue encontrado");

        private static Error overlap = new ("Alquiler.Overlap", "El alquiler está siendo tomado por dos o más clientes al mismo tiempo en la misma fecha");

        private static Error noReservado = new ("Alquiler.NotReserved", "El alquiler no esta reservado");

        private static Error noConfirmado = new ("Alquiler.NotConfirmed", "El alquiler no esta confirmado");

        private static Error yaEmpezo = new ("Alquiler.AlreadyStarted", "El alquiler ya ha comenzado");

        public static Error NotFound
        {
            get { return notFound; }
        }

        public static Error Overlap
        {
            get { return overlap; }
        }

        public static Error NoReservado
        {
            get { return noReservado; }
        }

        public static Error NoConfirmado
        {
            get { return noConfirmado; }
        }

        public static Error YaEmpezo
        {
            get { return yaEmpezo; }
        }
    }
}
