namespace CleanArchitecture.Domain.Users
{
    using System;
    using CleanArchitecture.Domain.Abstractions;
    using CleanArchitecture.Domain.Users.Events;

    public sealed class User : Entity
    {
        private User()
        {
        }

        private User(Guid id, string nombre, string apellido, string email)
            : base(id)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Email = email;
        }

        public string? Nombre { get; private set; }

        public string? Apellido { get; private set; }

        public string? Email { get; private set; }

        public static User Create(string nombre, string apellido, string email)
        {
            var user = new User(Guid.NewGuid(), nombre, apellido, email);

            user.RaiseDomainEvents(new UserCreatedDomainEvent(user.Id));

            return user;
        }
    }
}
