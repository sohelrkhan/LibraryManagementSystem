using LibraryManagementSystem.Models.DBModel;
using LibraryManagementSystem.Models.ViewModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class BorrowedBooksController : Controller
    {
        private readonly IBooks _books;
        private readonly IMembers _members;
        private readonly IBorrowedBooks _borrowedBooks;

        public BorrowedBooksController(IBooks books, IMembers members, IBorrowedBooks borrowedBooks)
        {
            _books = books;
            _members = members;
            _borrowedBooks = borrowedBooks;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _borrowedBooks.GetBorrowedBookList();
            return await Task.Run(() => View(result.Resource));
        }

        [HttpGet]
        public async Task<IActionResult> CreateUpdateBorrowedBook(int borrowID)
        {
            BorrowedBooksViewModel borrowedBook = new();

            if (borrowID == 0)
            {
                var memberResult = await _members.GetMemberList();
                ViewBag.Members = memberResult.Resource;

                var bookResult = await _books.GetBookList();
                ViewBag.Books = bookResult.Resource;

                return await Task.Run(() => View(borrowedBook));
            }
            else
            {
                var memberResult = await _members.GetMemberList();
                ViewBag.Members = memberResult.Resource;

                var bookResult = await _books.GetBookList();
                ViewBag.Books = bookResult.Resource;

                var result = await _borrowedBooks.GetBorrowedBookById(borrowID);
                return await Task.Run(() => View(result.Resource));
            }
        }
    }
}
