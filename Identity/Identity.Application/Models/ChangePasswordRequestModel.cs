namespace Identity.Application.Models
{
    public class ChangePasswordRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
