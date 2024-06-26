﻿using Dapper;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.CommonModel;
using LibraryManagementSystem.Models.DBModel;
using LibraryManagementSystem.Models.ViewModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.Repository.Repository
{
    public class BooksRepository : IBooks
    {
        private readonly ApplicationDbContext _db;

        private readonly IConfiguration? _configuration;
        private readonly string? _connectionString;

        public BooksRepository(ApplicationDbContext db, IConfiguration? configuration)
        {
            _db = db;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<CommonResponseModel<List<BooksViewModel?>>> GetBookList()
        {
            CommonResponseModel<List<BooksViewModel?>> commonResponseModel = new();
            List<BooksViewModel?> books = new();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = string.Format(DapperQuery.GetBookList);
                    var result = await connection.QueryAsync<BooksViewModel?>(query);

                    if (result != null && result.Any())
                    {
                        books = result.ToList();
                    }
                    else
                    {
                        books = new List<BooksViewModel?>();
                    }
                }

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

        public async Task<CommonResponseModel<BooksViewModel?>> GetBookById(int bookId)
        {
            CommonResponseModel<BooksViewModel?> commonResponseModel = new();
            BooksViewModel? book = new();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = string.Format(DapperQuery.GetBookById);
                    var result = await connection.QueryAsync<BooksViewModel?>(query, new { BookID = bookId });

                    if (result != null && result.Any())
                    {
                        book = result.FirstOrDefault();
                    }
                    else
                    {
                        book = new BooksViewModel();
                    }
                }

                commonResponseModel.Success = true;
                commonResponseModel.Resource = book;
                return commonResponseModel;
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Resource = book;
                commonResponseModel.Message = ex.Message;
                return commonResponseModel;
            }
        }

        public async Task<CommonResponseModel> CreateUpdateBook(Books model)
        {
            CommonResponseModel commonResponseModel = new();
            try
            {
                if (model != null)
                {
                    if (model.BookID == 0)
                    {
                        var createBook = _db.Books.Add(model);
                        await _db.SaveChangesAsync();

                        commonResponseModel.Success = true;
                        commonResponseModel.Message = ResponseMessage.SaveMessage;
                    }
                    else
                    {
                        var updateBook = _db.Books.Update(model);
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

        public async Task<CommonResponseModel> DeleteBook(int bookId)
        {
            CommonResponseModel commonResponseModel = new();
            try
            {
                if (bookId != 0)
                {
                    var book = await _db.Books.Where(x => x.BookID == bookId).FirstOrDefaultAsync();
                    if (book != null)
                    {
                        _db.Books.Remove(book);
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
