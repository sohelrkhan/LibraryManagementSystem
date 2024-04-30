using LibraryManagementSystem.Models.CommonModel;
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

        [HttpPost]
        public async Task<IActionResult> CreateUpdateBorrowedBook([FromBody] BorrowedBooks model)
        {
            var result = await _borrowedBooks.CreateUpdateBorrowedBook(model);

            if (result.Success == true)
            {
                return Json(new Confirmation { Message = result.Message, Output = "Success", ReturnValue = null });
            }
            else
            {
                return Json(new Confirmation { Message = result.Message, Output = "Error", ReturnValue = null });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBorrowedBook(int borrowID)
        {
            var result = await _borrowedBooks.DeleteBorrowedBook(borrowID);

            if (result.Success == true)
            {
                return Json(new Confirmation { Message = result.Message, Output = "Success", ReturnValue = null });
            }
            else
            {
                return Json(new Confirmation { Message = result.Message, Output = "Error", ReturnValue = null });
            }
        }
    }
}
