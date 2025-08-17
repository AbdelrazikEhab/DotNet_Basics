namespace DotNetAPI.Dots
{
    public partial class UserJobInfoDto
    {
        public int UserId { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }

        public UserJobInfoDto()
        {
            if (JobTitle == null) { JobTitle = ""; }
            if (Department == null) { Department = ""; }
        }

    }
}