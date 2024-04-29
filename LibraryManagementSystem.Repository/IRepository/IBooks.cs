using LibraryManagementSystem.Models.CommonModel;
using LibraryManagementSystem.Models.DBModel;
using LibraryManagementSystem.Models.ViewModel;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IBooks
    {
        Task<CommonResponseModel<List<BooksViewModel?>>> GetBookList();
        Task<CommonResponseModel<BooksViewModel?>> GetBookById(int bookId);
        Task<CommonResponseModel> CreateUpdateBook(Books model);
        Task<CommonResponseModel> DeleteBook(int bookId);
    }
}
