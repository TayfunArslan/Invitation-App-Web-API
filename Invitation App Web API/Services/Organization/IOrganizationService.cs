using System.Threading.Tasks;
using Invitation_App_Web_API.Models.ViewModels;
using Invitation_App_Web_API.Service;

namespace Invitation_App_Web_API.Services.Organization
{
    public interface IOrganizationService
    {
        Task<ServiceResult<OrganizationViewModel>> AddOrganization(OrganizationViewModel organizationModel, long userId);
        Task<ServiceResult<bool>> UpdateOrganization(OrganizationViewModel organizationViewModel, long userId);
    }
}
