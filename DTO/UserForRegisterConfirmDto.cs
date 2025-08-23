namespace DotNetAPI.Dots
{
    public partial class UserForRegisterConfirmDto
    {
        public string Email { get; set; }
        public string passwordHash { get; set; }
        public string passwordConfirm { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public UserForRegisterConfirmDto()
        {
            if (Email == null) { Email = ""; }
            if (passwordHash == null) { passwordHash = ""; }
            if (passwordConfirm == null) { passwordConfirm = ""; }
            if (FirstName == null) { FirstName = ""; }
            if (LastName == null) { LastName = ""; }
            if(Gender == null){Gender = "";}
        }

    }
}