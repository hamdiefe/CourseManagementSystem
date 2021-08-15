using CourseManagementSystem.Services.Grades.Dtos;

namespace CourseManagementSystem.Web.Models.Grades
{
    public class CreateOrEditGradeModalViewModel
    {
        public CreateOrEditGradeDto Grade { get; set; }

        public bool IsEditMode => Grade.Id.HasValue;
    }
}
