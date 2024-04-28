using LibraryManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooks _books;

        public BooksController(IBooks books)
        {
            _books = books;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _books.GetBookList();
            return await Task.Run(() => View(result.Resource));
        }
    }
}
