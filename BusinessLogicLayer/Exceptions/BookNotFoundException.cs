namespace BusinessLogicLayer.Exceptions;

public class BookNotFoundException : BusinessException
{
    public BookNotFoundException(int bookId) 
        : base($"Book with ID {bookId} was not found.")
    {
    }
}