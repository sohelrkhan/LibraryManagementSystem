using LibraryManagementSystem.Models.CommonModel;
using LibraryManagementSystem.Models.DBModel;
using LibraryManagementSystem.Models.ViewModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBooks _books;
        private readonly IAuthors _authors;

        public BooksController(IBooks books, IAuthors authors)
        {
            _books = books;
            _authors = authors;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _books.GetBookList();
            return await Task.Run(() => View(result.Resource));
        }

        [HttpGet]
        public async Task<IActionResult> CreateUpdateBook(int bookId)
        {
            BooksViewModel book = new();

            if (bookId == 0)
            {
                var result = await _authors.GetAuthorList();
                ViewBag.Authors = result.Resource;
                return await Task.Run(() => View(book));
            }
            else
            {
                var authors = await _authors.GetAuthorList();
                ViewBag.Authors = authors.Resource;
                var result = await _books.GetBookById(bookId);
                return await Task.Run(() => View(result.Resource));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateBook([FromBody] Books model)
        {
            var result = await _books.CreateUpdateBook(model);

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
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var result = await _books.DeleteBook(bookId);

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
