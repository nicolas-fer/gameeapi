using Domain.Validations;
using System.Text.RegularExpressions;

namespace Domain.Models
{
    public sealed class Team
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string PrimaryColor { get; private set; }
        public string SecondaryColor { get; private set; }

        public Team(string name, string primaryColor, string secondaryColor)
        {
            this.Name = name;
            this.PrimaryColor = primaryColor;   
            this.SecondaryColor = secondaryColor;

            Validate();
        }
        
        public Team(int id, string name, string primaryColor, string secondaryColor)
        {
            this.Id = id;

            DomainValidationException.When(id < 0, "Invalid id");

            this.Name = name;
            this.PrimaryColor = primaryColor;   
            this.SecondaryColor = secondaryColor;

            Validate();
        }

        private void Validate()
        {
            DomainValidationException.When(this.Name == null, "Name field cannot be empty");
            DomainValidationException.When(this.PrimaryColor == null, "PrimaryColor field cannot be empty");
            DomainValidationException.When(this.SecondaryColor == null, "SecondaryColor field cannot be empty");

            DomainValidationException.When(!Regex.IsMatch(this.PrimaryColor, "^#(?:[0-9a-fA-F]{3}){1,2}$"), "PrimaryColor is not a valid hex color");
            DomainValidationException.When(!Regex.IsMatch(this.SecondaryColor, "^#(?:[0-9a-fA-F]{3}){1,2}$"), "SecondaryColor is not a valid hex color");
        }
    }
}
