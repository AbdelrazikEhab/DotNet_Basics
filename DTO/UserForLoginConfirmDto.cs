namespace DotNetAPI.Dots
{
    public partial class UserForLoginConfirmDto
    {
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }

        public UserForLoginConfirmDto()
        {
            if (passwordHash == null) { passwordHash = new byte[0]; }
            if (passwordSalt == null) { passwordSalt = new byte[0]; }
        }

    }
}