using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.DBModel
{
    public class Authors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorID { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorBio { get; set; }
    }
}
