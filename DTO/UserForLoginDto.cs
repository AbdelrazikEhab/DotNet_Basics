namespace DotNetAPI.Dots
{
    public partial class UserForLoginDto
    {
        public string Email { get; set; }
        public string password { get; set; }

        public UserForLoginDto()
        {
            if (Email == null) { Email = ""; }
            if (password == null) { password = ""; }
        }

    }
}