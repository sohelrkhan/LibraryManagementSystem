using LibraryManagementSystem.Models.CommonModel;
using LibraryManagementSystem.Models.DBModel;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IAuthors
    {
        Task<CommonResponseModel<List<Authors>>> GetAuthorList();
        Task<CommonResponseModel<Authors>> GetAuthorById(int authorId);
        Task<CommonResponseModel> CreateUpdateAuthor(Authors model);
        Task<CommonResponseModel> DeleteAuthor(int authorId);
    }
}
