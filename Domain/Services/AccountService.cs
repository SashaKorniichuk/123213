using AutoMapper;
using Data.Entities;
using Domain.ApiModel.ResponseApiModels;
using Domain.Errors;
using Domain.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public AccountService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> ChangeEmail(string newEmail, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }
            user.UserName = newEmail;
            user.Email = newEmail;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("EmailChangeSuccess"));
            }
            else
            {
                List<IdentityError> identityErrors = result.Errors.ToList();
                var errors = string.Join(" ", identityErrors.Select(x => x.Description));

                throw new RestException(HttpStatusCode.BadRequest, errors);
            }
        }

        public async Task<ResponseApiModel<HttpStatusCode>> ChangePassword(string newPassword, string oldPassword, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (result.Succeeded)
            {
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("PasswordChangeSuccess"));
            }
            else
            {
                List<IdentityError> identityErrors = result.Errors.ToList();
                var errors = string.Join(" ", identityErrors.Select(x => x.Description));

                throw new RestException(HttpStatusCode.BadRequest, errors);
            }
        }

        public async Task<PersonalInformationResponseApiModel> GetPersonalInformation(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var personalInformation = _mapper.Map<PersonalInformationResponseApiModel>(user);

            return personalInformation;
        }
    }
}
