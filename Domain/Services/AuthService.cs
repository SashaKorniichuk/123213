using AutoMapper;
using Data.Entities;
using Domain.ApiModel.RequestApiModels;
using Domain.ApiModel.ResponseApiModels;
using Domain.Errors;
using Domain.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> RegisterUser(RegisterRequestApiModel userRequest)
        {
            var user = _mapper.Map<User>(userRequest);

            var result = await _userManager.CreateAsync(user, userRequest.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "participant");

                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("RegistrationSucceeded"));

            }
            else
            {
                List<IdentityError> identityErrors = result.Errors.ToList();
                var errors = string.Join(" ", identityErrors.Select(x => x.Description));

                throw new RestException(HttpStatusCode.BadRequest, errors);
            }
        }

        public async Task<AuthenticateResponseApiModel> LoginUser(LoginRequestApiModel userRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(userRequest.Email, userRequest.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(userRequest.Email);

                if (user == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("UserNotFound"));
                }

                var roles = await _userManager.GetRolesAsync(user);
                var token = await _jwtService.CreateJwtToken(user);

                await _signInManager.SignInAsync(user, false);

                return new AuthenticateResponseApiModel(user.Email, token, roles.FirstOrDefault());
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("LoginWrongCredentials"));
            }
        }
    }
}
