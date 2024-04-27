using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Repository.IRepository;
using LibraryManagementSystem.Models.DBModel;

namespace LibraryManagementSystem.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthors _authors;
        public AuthorsController(IAuthors authors)
        {
            _authors = authors;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _authors.GetAuthorList();
            return await Task.Run(() => View(result));
        }

        [HttpGet]
        public async Task<IActionResult> CreateUpdateAuthor(int authorId)
        {
            Authors authors = new();
            
            if(authorId == 0)
            {
                return await Task.Run(() => View(authors));
            }
            else
            {
                return await Task.Run(() => View(authors));
            }
            
        }
    }
}
