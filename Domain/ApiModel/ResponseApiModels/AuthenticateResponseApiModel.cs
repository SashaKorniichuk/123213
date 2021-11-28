using System.ComponentModel.DataAnnotations;

namespace Domain.ApiModel.ResponseApiModels
{
    public class AuthenticateResponseApiModel
    {
        public AuthenticateResponseApiModel(string email = null, string token = null, string role = null)
        {
            Token = token;
            Email = email;
            Role = role;
        }

        [Required]
        public string Token { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
