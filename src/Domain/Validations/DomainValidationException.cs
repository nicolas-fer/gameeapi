namespace Domain.Validations
{
    public class DomainValidationException : Exception
    {
        public DomainValidationException(string message) : base(message)
        {

        }

        public static void ThrowWhen(bool condition, string message)
        {
            if (condition)
                throw new DomainValidationException(message);
        } 
        
        public static void ThrowWhenNot(bool condition, string message)
        {
            if (!condition)
                throw new DomainValidationException(message);
        }
    }
}
