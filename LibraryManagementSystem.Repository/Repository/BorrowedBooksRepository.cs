using Dapper;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.CommonModel;
using LibraryManagementSystem.Models.DBModel;
using LibraryManagementSystem.Models.ViewModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repository.Repository
{
    public class BorrowedBooksRepository : IBorrowedBooks
    {
        private readonly ApplicationDbContext _db;

        private readonly IConfiguration? _configuration;
        private readonly string? _connectionString;

        public BorrowedBooksRepository(ApplicationDbContext db, IConfiguration? configuration)
        {
            _db = db;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<CommonResponseModel<List<BorrowedBooksViewModel?>>> GetBorrowedBookList()
        {
            CommonResponseModel<List<BorrowedBooksViewModel?>> commonResponseModel = new();
            List<BorrowedBooksViewModel?> borrowedBooks = new();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = string.Format(DapperQuery.GetBorrowedBookList);
                    var result = await connection.QueryAsync<BorrowedBooksViewModel?>(query);

                    if (result != null && result.Any())
                    {
                        borrowedBooks = result.ToList();
                    }
                    else
                    {
                        borrowedBooks = new List<BorrowedBooksViewModel?>();
                    }
                }

                commonResponseModel.Success = true;
                commonResponseModel.Resource = borrowedBooks;
                return commonResponseModel;
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Resource = borrowedBooks;
                commonResponseModel.Message = ex.Message;
                return commonResponseModel;
            }
        }

        public async Task<CommonResponseModel<BorrowedBooksViewModel?>> GetBorrowedBookById(int borrowID)
        {
            CommonResponseModel<BorrowedBooksViewModel?> commonResponseModel = new();
            BorrowedBooksViewModel? borrowedBook = new();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = string.Format(DapperQuery.GetBorrowedBookById);
                    var result = await connection.QueryAsync<BorrowedBooksViewModel?>(query, new { BorrowID = borrowID });

                    if (result != null && result.Any())
                    {
                        borrowedBook = result.FirstOrDefault();
                    }
                    else
                    {
                        borrowedBook = new BorrowedBooksViewModel();
                    }
                }

                commonResponseModel.Success = true;
                commonResponseModel.Resource = borrowedBook;
                return commonResponseModel;
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Resource = borrowedBook;
                commonResponseModel.Message = ex.Message;
                return commonResponseModel;
            }
        }

        public async Task<CommonResponseModel> CreateUpdateBorrowedBook(BorrowedBooks model)
        {
            CommonResponseModel commonResponseModel = new();
            try
            {
                if (model != null)
                {
                    if (model.BorrowID == 0)
                    {
                        var createBorrowedBook = _db.BorrowedBooks.Add(model);
                        await _db.SaveChangesAsync();

                        commonResponseModel.Success = true;
                        commonResponseModel.Message = ResponseMessage.SaveMessage;
                    }
                    else
                    {
                        var updateBorrowedBook = _db.BorrowedBooks.Update(model);
                        await _db.SaveChangesAsync();

                        commonResponseModel.Success = true;
                        commonResponseModel.Message = ResponseMessage.UpdateMessage;
                    }
                }
                else
                {
                    commonResponseModel.Success = false;
                    commonResponseModel.Message = ResponseMessage.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Message = ex.ToString();
            }
            return commonResponseModel;
        }

        public async Task<CommonResponseModel> DeleteBorrowedBook(int borrowID)
        {
            CommonResponseModel commonResponseModel = new();
            try
            {
                if (borrowID != 0)
                {
                    var borrowedbook = await _db.BorrowedBooks.Where(x => x.BorrowID == borrowID).FirstOrDefaultAsync();
                    if (borrowedbook != null)
                    {
                        _db.BorrowedBooks.Remove(borrowedbook);
                        await _db.SaveChangesAsync();

                        commonResponseModel.Success = true;
                        commonResponseModel.Message = ResponseMessage.DeleteMessage;
                    }
                    else
                    {
                        commonResponseModel.Success = false;
                        commonResponseModel.Message = ResponseMessage.ErrorMessage;
                    }
                }
                else
                {
                    commonResponseModel.Success = false;
                    commonResponseModel.Message = ResponseMessage.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Message = ex.ToString();
            }
            return commonResponseModel;
        }
    }
}
