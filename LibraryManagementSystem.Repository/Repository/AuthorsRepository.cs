using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.CommonModel;
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

        public async Task<CommonResponseModel<List<Authors>>> GetAuthorList()
        {
            CommonResponseModel<List<Authors>> commonResponseModel = new();
            List<Authors> authors = new();
            try
            {
                authors = await _db.Authors.ToListAsync();

                commonResponseModel.Success = true;
                commonResponseModel.Resource = authors;
                return commonResponseModel;
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Resource = authors;
                commonResponseModel.Message = ex.Message;
                return commonResponseModel;
            }
        }

        public async Task<CommonResponseModel<Authors>> GetAuthorById(int authorId)
        {
            CommonResponseModel<Authors> commonResponseModel = new();
            Authors? author = new();
            try
            {
                author = await _db.Authors.Where(x => x.AuthorID == authorId).FirstOrDefaultAsync();

                commonResponseModel.Success = true;
                commonResponseModel.Resource = author;
                return commonResponseModel;
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Resource = author;
                commonResponseModel.Message = ex.Message;
                return commonResponseModel;
            }
        }

        public async Task<CommonResponseModel> CreateUpdateAuthor(Authors model)
        {
            CommonResponseModel commonResponseModel = new();
            try
            {
                if(model != null)
                {
                    if(model.AuthorID == 0)
                    {
                        var createAuthor = _db.Authors.Add(model);
                        await _db.SaveChangesAsync();

                        commonResponseModel.Success = true;
                        commonResponseModel.Message = ResponseMessage.SaveMessage;
                    }
                    else
                    {
                        var updateAuthor = _db.Authors.Update(model);
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

        public async Task<CommonResponseModel> DeleteAuthor(int authorId)
        {
            CommonResponseModel commonResponseModel = new();
            try
            {
                if (authorId != 0)
                {
                    var author = await _db.Authors.Where(x => x.AuthorID == authorId).FirstOrDefaultAsync();
                    if (author != null)
                    {
                        _db.Authors.Remove(author);
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
