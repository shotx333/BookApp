namespace BusinessLogicLayer.Exceptions;

public class DuplicateBookException : BusinessException
{
    public DuplicateBookException(string title, int authorId) 
        : base($"A book with title '{title}' already exists for author ID {authorId}.")
    {
    }
}