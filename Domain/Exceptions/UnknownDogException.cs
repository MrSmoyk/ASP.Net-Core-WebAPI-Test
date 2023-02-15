namespace Domain.Exceptions
{
    public class UnknownDogException : BaseBadRequestException
    {
        public UnknownDogException(string? message) : base(message)
        {
        }
    }
}
