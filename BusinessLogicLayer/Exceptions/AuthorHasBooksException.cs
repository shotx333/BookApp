namespace BusinessLogicLayer.Exceptions;

public class AuthorHasBooksException : BusinessException
{
    public AuthorHasBooksException(int authorId, int bookCount) 
        : base($"Cannot delete author with ID {authorId} because they have {bookCount} associated books. Please delete or reassign the books first.")
    {
    }
}