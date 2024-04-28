using LibraryManagementSystem.Models.CommonModel;
using LibraryManagementSystem.Models.DBModel;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IBooks
    {
        Task<CommonResponseModel<List<Books>>> GetBookList();
    }
}
