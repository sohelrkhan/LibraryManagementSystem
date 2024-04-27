using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.DBModel
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public int AuthorID { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? AvailableCopies { get; set; }
        public int? TotalCopies { get; set; }
    }
}
