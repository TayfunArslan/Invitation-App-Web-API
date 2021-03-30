using System.Threading.Tasks;
using Invitation_App_Web_API.Models.RequestModels;
using Invitation_App_Web_API.Models.ViewModels;
using Invitation_App_Web_API.Service;

namespace Invitation_App_Web_API.Services.User
{
    public interface IUserService
    {
        Task<ServiceResult<UserViewModel>> Login(LoginRequestModel model);
        Task<ServiceResult<UserViewModel>> AddUser(UserViewModel userModel);
        Task<ServiceResult<bool>> DeleteUser(int userId);
    }
}
