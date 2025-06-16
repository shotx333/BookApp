namespace BusinessLogicLayer.Exceptions;

public class AuthorNotFoundException : BusinessException
{
    public AuthorNotFoundException(int authorId) 
        : base($"Author with ID {authorId} was not found.")
    {
    }
}