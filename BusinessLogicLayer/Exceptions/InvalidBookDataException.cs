namespace BusinessLogicLayer.Exceptions;

public class InvalidBookDataException : BusinessException
{
    public InvalidBookDataException(string message) 
        : base($"Invalid book data: {message}")
    {
    }
}