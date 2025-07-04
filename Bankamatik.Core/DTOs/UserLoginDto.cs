namespace Bankamatik.Core.DTOs
{
    public class UserLoginDTO
    {
        //login insert update için
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
