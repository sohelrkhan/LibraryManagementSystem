using LibraryManagementSystem.Models.DBModel;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IAuthors
    {
        Task<List<Authors>> GetAuthorList();
    }
}
