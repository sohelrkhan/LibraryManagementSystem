using LibraryManagementSystem.Models.CommonModel;
using LibraryManagementSystem.Models.DBModel;
using LibraryManagementSystem.Models.ViewModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly IMembers _members;
        public MembersController(IMembers members)
        {
            _members = members;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _members.GetMemberList();
            return await Task.Run(() => View(result.Resource));
        }

        [HttpGet]
        public async Task<IActionResult> CreateUpdateMember(int memberId)
        {
            Members member = new();

            if (memberId == 0)
            {
                return await Task.Run(() => View(member));
            }
            else
            {
                var result = await _members.GetMemberById(memberId);
                return await Task.Run(() => View(result.Resource));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateMember([FromBody] Members model)
        {
            var result = await _members.CreateUpdateMember(model);

            if (result.Success == true)
            {
                return Json(new Confirmation { Message = result.Message, Output = "Success", ReturnValue = null });
            }
            else
            {
                return Json(new Confirmation { Message = result.Message, Output = "Error", ReturnValue = null });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMember(int memberId)
        {
            var result = await _members.DeleteMember(memberId);

            if (result.Success == true)
            {
                return Json(new Confirmation { Message = result.Message, Output = "Success", ReturnValue = null });
            }
            else
            {
                return Json(new Confirmation { Message = result.Message, Output = "Error", ReturnValue = null });
            }
        }
    }
}
