using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.DBModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository.Repository
{
    public class AuthorsRepository : IAuthors
    {
        private readonly ApplicationDbContext _db;

        public AuthorsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Authors>> GetAuthorList()
        {
            try
            {
                var authors = await _db.Authors.ToListAsync();
                return authors;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
