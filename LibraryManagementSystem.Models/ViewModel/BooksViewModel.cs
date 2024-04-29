namespace LibraryManagementSystem.Models.ViewModel
{
    public class BooksViewModel
    {
        public int BookID { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public int AuthorID { get; set; }
        public string? AuthorName { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? PublishedDateString { get; set; }
        public int? AvailableCopies { get; set; }
        public int? TotalCopies { get; set; }
    }
}
