namespace LibraryManagementSystem.Models.CommonModel
{
    public static class DapperQuery
    {
        public const string GetBookList = "Select B.BookID, B.Title, B.ISBN, B.AuthorID, A.AuthorName, B.PublishedDate, FORMAT(B.PublishedDate, 'dd MMMM, yyyy') AS PublishedDateString, B.AvailableCopies, B.TotalCopies From Books AS B With(NoLock) LEFT JOIN Authors AS A With(NoLock) on B.AuthorID = A.AuthorID";

        public const string GetBookById = "Select B.BookID, B.Title, B.ISBN, B.AuthorID, A.AuthorName, B.PublishedDate, FORMAT(B.PublishedDate, 'dd MMMM, yyyy') AS PublishedDateString, B.AvailableCopies, B.TotalCopies From Books AS B With(NoLock) LEFT JOIN Authors AS A With(NoLock) on B.AuthorID = A.AuthorID Where B.BookID = @BookID";

        public const string GetBorrowedBookList = "Select BB.BorrowID, BB.MemberID, CONCAT(M.FirstName, ' ' ,M.LastName) AS MemberName, BB.BookID, B.Title AS BookTitle, BB.BorrowDate, FORMAT(BB.BorrowDate, 'dd MMMM, yyyy') AS BorrowDateString, BB.ReturnDate, FORMAT(BB.ReturnDate, 'dd MMMM, yyyy') AS ReturnDateString, BB.Status From BorrowedBooks AS BB With(NoLock) LEFT JOIN Members AS M With(NoLock) on BB.MemberID = M.MemberID LEFT JOIN Books AS B With(NoLock) on BB.BookID = B.BookID";
        
        public const string GetBorrowedBookById = "Select BB.BorrowID, BB.MemberID, CONCAT(M.FirstName, ' ' ,M.LastName) AS MemberName, BB.BookID, B.Title AS BookTitle, BB.BorrowDate, FORMAT(BB.BorrowDate, 'dd MMMM, yyyy') AS BorrowDateString, BB.ReturnDate, FORMAT(BB.ReturnDate, 'dd MMMM, yyyy') AS ReturnDateString, BB.Status From BorrowedBooks AS BB With(NoLock) LEFT JOIN Members AS M With(NoLock) on BB.MemberID = M.MemberID LEFT JOIN Books AS B With(NoLock) on BB.BookID = B.BookID Where BB.BorrowID = @BorrowID";

    }
}
