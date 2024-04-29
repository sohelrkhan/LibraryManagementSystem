using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models.CommonModel
{
    public static class DapperQuery
    {
        public const string GetBookList = "Select B.BookID, B.Title, B.ISBN, B.AuthorID, A.AuthorName, B.PublishedDate, FORMAT(B.PublishedDate, 'dd MMMM, yyyy') AS PublishedDateString, B.AvailableCopies, B.TotalCopies From Books AS B With(NoLock) LEFT JOIN Authors AS A With(NoLock) on B.AuthorID = A.AuthorID";

        public const string GetBookById = "Select B.BookID, B.Title, B.ISBN, B.AuthorID, A.AuthorName, B.PublishedDate, FORMAT(B.PublishedDate, 'dd MMMM, yyyy') AS PublishedDateString, B.AvailableCopies, B.TotalCopies From Books AS B With(NoLock) LEFT JOIN Authors AS A With(NoLock) on B.AuthorID = A.AuthorID Where B.BookID = @BookID";
    }
}
