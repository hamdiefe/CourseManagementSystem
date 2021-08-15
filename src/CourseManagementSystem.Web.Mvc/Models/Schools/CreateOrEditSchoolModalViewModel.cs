using CourseManagementSystem.Services.Schools.Dtos;

namespace CourseManagementSystem.Web.Models.Schools
{
    public class CreateOrEditSchoolModalViewModel
    {
        public CreateOrEditSchoolDto School { get; set; }

        public bool IsEditMode => School.Id.HasValue;
    }
}
