using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.MySettings.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
