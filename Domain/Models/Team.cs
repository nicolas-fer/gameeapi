using Domain.Validations;
using System.Text.RegularExpressions;

namespace Domain.Models
{
    public sealed class Team
    {
        public int Id { get; }
        public string Name { get; }
        public string PrimaryColor { get; }
        public string SecondaryColor { get; }

        public Team(string name, string primaryColor, string secondaryColor)
        {
            Name = name;
            PrimaryColor = primaryColor;   
            SecondaryColor = secondaryColor;

            Validate();
        }
        
        public Team(int id, string name, string primaryColor, string secondaryColor)
        {
            Id = id;

            DomainValidationException.ThrowWhen(id < 0, "Invalid id");

            Name = name;
            PrimaryColor = primaryColor;   
            SecondaryColor = secondaryColor;

            Validate();
        }

        private void Validate()
        {
            DomainValidationException.ThrowWhen(!Regex.IsMatch(PrimaryColor, "^#(?:[0-9a-fA-F]{3}){1,2}$"), "PrimaryColor is not a valid hex color");
            DomainValidationException.ThrowWhen(!Regex.IsMatch(SecondaryColor, "^#(?:[0-9a-fA-F]{3}){1,2}$"), "SecondaryColor is not a valid hex color");
        }
    }
}
