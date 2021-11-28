using Domain.ApiModel.RequestApiModels;
using Domain.ApiModel.ResponseApiModels;
using System.Net;

namespace Domain.Services.Abstractions
{
    public interface IAuthService
    {
        Task<ResponseApiModel<HttpStatusCode>> RegisterUser(RegisterRequestApiModel user);
        Task<AuthenticateResponseApiModel> LoginUser(LoginRequestApiModel userRequest);
    }
}
