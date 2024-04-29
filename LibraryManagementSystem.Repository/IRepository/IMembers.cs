using LibraryManagementSystem.Models.CommonModel;
using LibraryManagementSystem.Models.DBModel;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IMembers
    {
        Task<CommonResponseModel<List<Members>>> GetMemberList();
        Task<CommonResponseModel<Members>> GetMemberById(int memberId);
        Task<CommonResponseModel> CreateUpdateMember(Members model);
        Task<CommonResponseModel> DeleteMember(int memberId);
    }
}
