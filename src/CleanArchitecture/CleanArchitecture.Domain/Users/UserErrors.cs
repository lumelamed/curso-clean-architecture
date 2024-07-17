namespace CleanArchitecture.Domain.Users
{
    using CleanArchitecture.Domain.Abstractions;

    public static class UserErrors
    {
        private static Error notFound = new ("User.Found", "El usuario con el Id especificado no fue encontrado");

        private static Error invalidCredentials = new ("User.InvalidCredentials", "Las credenciales son incorrectas");

        public static Error NotFound
        {
            get { return notFound; }
        }

        public static Error InvalidCredentials
        {
            get { return invalidCredentials; }
        }
    }
}
