using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Repository.IRepository;
using LibraryManagementSystem.Models.DBModel;
using LibraryManagementSystem.Models.CommonModel;

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
            return await Task.Run(() => View(result.Resource));
        }

        [HttpGet]
        public async Task<IActionResult> CreateUpdateAuthor(int authorId)
        {
            Authors authors = new();

            if (authorId == 0)
            {
                return await Task.Run(() => View(authors));
            }
            else
            {
                var result = await _authors.GetAuthorById(authorId);
                return await Task.Run(() => View(result.Resource));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateAuthor([FromBody] Authors model)
        {
            var result = await _authors.CreateUpdateAuthor(model);

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
        public async Task<IActionResult> DeleteAuthor(int authorId)
        {
            var result = await _authors.DeleteAuthor(authorId);

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
