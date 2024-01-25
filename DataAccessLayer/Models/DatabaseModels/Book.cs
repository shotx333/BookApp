using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.DatabaseModels;

public class Book
{
    [Key] public int Id { get; set; }

    public string Title { get; set; }
    public string Genre { get; set; }


    public int AuthorId { get; set; } // Foreign key property
    public Author? Author { get; set; }
}