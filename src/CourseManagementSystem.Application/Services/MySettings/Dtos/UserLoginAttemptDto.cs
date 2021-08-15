using System;

namespace CourseManagementSystem.Services.MySettings.Dtos
{
    public class UserLoginAttemptDto
    {
        public string BrowserInfo { get; set; }
        public string ClientIpAddress { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}
