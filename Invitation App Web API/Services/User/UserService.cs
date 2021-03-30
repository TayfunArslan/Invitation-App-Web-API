using System;
using System.Threading.Tasks;
using AutoMapper;
using Invitation_App_Web_API.Data.UnitOfWork;
using Invitation_App_Web_API.Enums;
using Invitation_App_Web_API.Helpers;
using Invitation_App_Web_API.Models.RequestModels;
using Invitation_App_Web_API.Models.ViewModels;
using Invitation_App_Web_API.Service;
using Microsoft.Extensions.Logging;

namespace Invitation_App_Web_API.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork uow, IMapper mapper, ILogger<UserService> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<UserViewModel>> Login(LoginRequestModel model)
        {
            var serviceResult = new ServiceResult<UserViewModel>();
            int? errorCode = null;

            try
            {
                var user = await _uow.UserRepository.GetSingleAsync(u =>
                    u.Email == model.Email || u.PhoneNumber == model.PhoneNumber);

                if (user == null)
                {
                    errorCode = (int)ErrorCodes.UserNotFound;
                    throw new Exception("User not found");
                }

                var isPasswordCorrect = HashCalculator.ValidatePassword(model.Password, user.Password);

                if (!isPasswordCorrect)
                {
                    errorCode = (int)ErrorCodes.IncorrectPassword;
                    throw new Exception("Password is not correct");
                }

                serviceResult.Data = _mapper.Map<UserViewModel>(user);
                serviceResult.ServiceResultType = ServiceResultType.Ok;
            }
            catch (Exception e)
            {
                errorCode ??= (int)ErrorCodes.UnknownError;

                _logger.LogError(e, "Exception@AddUser");
                serviceResult.ServiceResultType = ServiceResultType.Fail;
                serviceResult.ErrorModel = new ErrorModel()
                {
                    ErrorCode = errorCode.Value,
                    ErrorMessage = e.Message
                };
            }

            return serviceResult;
        }

        public async Task<ServiceResult<UserViewModel>> AddUser(UserViewModel userModel)
        {
            var serviceResult = new ServiceResult<UserViewModel>();
            int? errorCode = null;

            try
            {
                var isExist = await _uow.UserRepository.GetSingleAsync(u =>
                    u.PhoneNumber == userModel.PhoneNumber || u.Email == userModel.Email && u.IsActive && !u.IsDeleted) != null;

                if (isExist)
                {
                    errorCode = (int)ErrorCodes.UserExist;
                    throw new Exception("User already exist");
                }

                var user = _mapper.Map<Data.Entities.User>(userModel);
                user.IsActive = true;
                user.IsDeleted = false;
                user.CreateDateTime = DateTime.Now;
                user.Password = HashCalculator.HashPassword(userModel.Password);

                await _uow.UserRepository.AddAsync(user);

                var result = await _uow.SaveChangesAsync();

                if (result == -1)
                {
                    errorCode = (int)ErrorCodes.DbError;
                    throw new Exception("Failed to register user");
                }

                serviceResult.Data = _mapper.Map<UserViewModel>(user);
                serviceResult.ServiceResultType = ServiceResultType.Ok;
            }
            catch (Exception e)
            {
                errorCode ??= (int)ErrorCodes.UnknownError;

                _logger.LogError(e, "Exception@AddUser");
                serviceResult.ServiceResultType = ServiceResultType.Fail;
                serviceResult.ErrorModel = new ErrorModel()
                {
                    ErrorCode = errorCode.Value,
                    ErrorMessage = e.Message
                };
            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteUser(int userId)
        {
            var serviceResult = new ServiceResult<bool>();
            int? errorCode = null;

            try
            {
                var user = await _uow.UserRepository.GetSingleAsync(u => u.Id == userId && u.IsActive && !u.IsDeleted);

                user.IsActive = false;
                user.IsDeleted = true;

                var result = await _uow.SaveChangesAsync();

                if (result == -1)
                {
                    errorCode = (int) ErrorCodes.DbError;
                    throw new Exception("Db Error");
                }

                serviceResult.Data = true;
                serviceResult.ServiceResultType = ServiceResultType.Ok;
            }
            catch (Exception e)
            {
                errorCode ??= (int)ErrorCodes.UnknownError;
                _logger.LogError(e, "Exception@DeleteUser");

                serviceResult.ServiceResultType = ServiceResultType.Fail;
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
