using LibraryManagementSystem.Models.CommonModel;
using LibraryManagementSystem.Models.DBModel;
using LibraryManagementSystem.Models.ViewModel;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IBorrowedBooks
    {
        Task<CommonResponseModel<List<BorrowedBooksViewModel?>>> GetBorrowedBookList();
        Task<CommonResponseModel<BorrowedBooksViewModel?>> GetBorrowedBookById(int borrowID);
        Task<CommonResponseModel> CreateUpdateBorrowedBook(BorrowedBooks model);
        Task<CommonResponseModel> DeleteBorrowedBook(int borrowID);
    }
}
