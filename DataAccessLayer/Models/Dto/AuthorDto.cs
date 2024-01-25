
namespace DataAccessLayer.Models.DTO
{
    public class AuthorDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<BookDto> Books { get; set; }

    }
}
