namespace Domain.ApiModel.RequestApiModels
{
    public class ChangePasswordRequestApiModel
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }

    }
}
