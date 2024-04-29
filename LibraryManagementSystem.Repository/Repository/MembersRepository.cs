using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.CommonModel;
using LibraryManagementSystem.Models.DBModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.Repository.Repository
{
    public class MembersRepository : IMembers
    {
        private readonly ApplicationDbContext _db;

        private readonly IConfiguration? _configuration;
        private readonly string? _connectionString;

        public MembersRepository(ApplicationDbContext db, IConfiguration? configuration)
        {
            _db = db;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<CommonResponseModel<List<Members>>> GetMemberList()
        {
            CommonResponseModel<List<Members>> commonResponseModel = new();
            List<Members> members = new();
            try
            {
                members = await _db.Members.ToListAsync();

                commonResponseModel.Success = true;
                commonResponseModel.Resource = members;
                return commonResponseModel;
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Resource = members;
                commonResponseModel.Message = ex.Message;
                return commonResponseModel;
            }
        }

        public async Task<CommonResponseModel<Members>> GetMemberById(int memberId)
        {
            CommonResponseModel<Members> commonResponseModel = new();
            Members? member = new();
            try
            {
                member = await _db.Members.Where(x => x.MemberID == memberId).FirstOrDefaultAsync();

                commonResponseModel.Success = true;
                commonResponseModel.Resource = member;
                return commonResponseModel;
            }
            catch (Exception ex)
            {
                commonResponseModel.Success = false;
                commonResponseModel.Resource = member;
                commonResponseModel.Message = ex.Message;
                return commonResponseModel;
            }
        }

        public async Task<CommonResponseModel> CreateUpdateMember(Members model)
        {
            CommonResponseModel commonResponseModel = new();
            try
            {
                if (model != null)
                {
                    if (model.MemberID == 0)
                    {
                        var createMember = _db.Members.Add(model);
                        await _db.SaveChangesAsync();

                        commonResponseModel.Success = true;
                        commonResponseModel.Message = ResponseMessage.SaveMessage;
                    }
                    else
                    {
                        var updateMember = _db.Members.Update(model);
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

        public async Task<CommonResponseModel> DeleteMember(int memberId)
        {
            CommonResponseModel commonResponseModel = new();
            try
            {
                if (memberId != 0)
                {
                    var member = await _db.Members.Where(x => x.MemberID == memberId).FirstOrDefaultAsync();
                    if (member != null)
                    {
                        _db.Members.Remove(member);
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
