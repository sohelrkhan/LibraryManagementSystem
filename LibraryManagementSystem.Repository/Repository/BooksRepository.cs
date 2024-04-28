using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.CommonModel;
using LibraryManagementSystem.Models.DBModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repository.Repository
{
    public class BooksRepository : IBooks
    {
        private readonly ApplicationDbContext _db;

        public BooksRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CommonResponseModel<List<Books>>> GetBookList()
        {
            CommonResponseModel<List<Books>> commonResponseModel = new();
            List<Books> books = new();
            try
            {
                books = await _db.Books.ToListAsync();

                commonResponseModel.Success = true;
                commonResponseModel.Resource = books;
                return commonResponseModel;
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Resource = books;
                commonResponseModel.Message = ex.Message;
                return commonResponseModel;
            }
        }
    }
}
