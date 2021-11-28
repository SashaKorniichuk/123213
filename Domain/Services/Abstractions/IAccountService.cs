using Domain.ApiModel.ResponseApiModels;
using System.Net;

namespace Domain.Services.Abstractions
{
    public interface IAccountService
    {
        Task<ResponseApiModel<HttpStatusCode>> ChangePassword(string newPassword, string oldPassword, string userId);
        Task<ResponseApiModel<HttpStatusCode>> ChangeEmail(string newEmail,string userId);
        Task<PersonalInformationResponseApiModel> GetPersonalInformation(string userId);
    }
}
