using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}