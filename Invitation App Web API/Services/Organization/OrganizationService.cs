using System;
using System.Threading.Tasks;
using AutoMapper;
using Invitation_App_Web_API.Data.UnitOfWork;
using Invitation_App_Web_API.Enums;
using Invitation_App_Web_API.Models.ViewModels;
using Invitation_App_Web_API.Service;
using Microsoft.Extensions.Logging;

namespace Invitation_App_Web_API.Services.Organization
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationService> _logger;

        public OrganizationService(IUnitOfWork uow, IMapper mapper, ILogger<OrganizationService> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<OrganizationViewModel>> AddOrganization(OrganizationViewModel organizationModel, long userId)
        {
            var serviceResult = new ServiceResult<OrganizationViewModel>();
            int? errorCode = null;

            try
            {
                var isExist = await
                    _uow.OrganizationRepository.GetSingleAsync(o =>
                        o.Code == organizationModel.Code && o.IsActive && !o.IsDeleted) != null;

                if (isExist)
                {
                    errorCode = (int)ErrorCodes.OrganizationCodeExist;
                    throw new Exception("Organization code already used");
                }

                organizationModel.CreatorId = userId;
                organizationModel.IsActive = true;
                organizationModel.IsDeleted = false;

                var organization = _mapper.Map<Data.Entities.Organization>(organizationModel);

                await _uow.OrganizationRepository.AddAsync(organization);

                var result = await _uow.SaveChangesAsync();
                if (result == -1)
                {
                    errorCode = (int)ErrorCodes.DbError;
                    throw new Exception("Db error");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception@AddOrganization");
                errorCode ??= (int)ErrorCodes.UnknownError;

                serviceResult.ErrorModel = new ErrorModel()
                {
                    ErrorCode = errorCode.Value,
                    ErrorMessage = e.Message
                };
            }


            return serviceResult;
        }

        public async Task<ServiceResult<bool>> UpdateOrganization(OrganizationViewModel organizationViewModel, long userId)
        {
            var serviceResult = new ServiceResult<bool>();
            int? errorCode = null;

            try
            {
                //OrganizationCoOwner'lar da değiştirebilecekler.
                if (organizationViewModel.CreatorId != userId)
                {
                    errorCode = (int)ErrorCodes.NotAuthorized;
                    throw new Exception("You are not authorized");
                }

                var organization =
                   await _uow.OrganizationRepository.GetSingleAsync(o =>
                        o.Id == organizationViewModel.Id && o.IsActive && !o.IsDeleted);

                if (organization == null)
                {
                    errorCode = (int)ErrorCodes.OrganizationNotFound;
                    throw new Exception("Organization not found");
                }

                if (organizationViewModel.StartDateTime.HasValue)
                    organization.StartDateTime = organization.StartDateTime;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception@UpdateOrganization");
                errorCode ??= (int)ErrorCodes.UnknownError;

                serviceResult.ErrorModel = new ErrorModel()
                {
                    ErrorCode = errorCode.Value,
                    ErrorMessage = e.Message
                };
            }

            return serviceResult;
        }
    }
}
