namespace LibraryManagementSystem.Models.ViewModel
{
    public class BorrowedBooksViewModel
    {
        public int BorrowID { get; set; }
        public int MemberID { get; set; }
        public string? MemberName { get; set; }
        public int BookID { get; set; }
        public string? BookTitle { get; set; }
        public DateTime? BorrowDate { get; set; }
        public string? BorrowDateString { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? ReturnDateString { get; set; }
        public string? Status { get; set; }
    }
}
