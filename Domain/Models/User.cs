using Domain.Validations;

namespace Domain.Models
{
    public sealed class User
    {
        public int Id { get; }
        public string Username { get; }
        public string Password { get; }
        public string Role { get; }

        public User(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;

            Validate();
        }

        public User(int id, string username, string password, string role)
        {
            Id = id;

            DomainValidationException.ThrowWhen(id < 0, "Invalid id");

            Username = username;
            Password = password;
            Role = role;
        }

        private void Validate()
        {
            // TODO: Criar validação do tipo de autorização
        }
    }
}
