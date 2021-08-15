using CourseManagementSystem.Services.Parents.Dtos;

namespace CourseManagementSystem.Web.Models.Students
{
    public class AddStudentParentFieldViewModel
    {
        public CreateOrEditParentDto Parent { get; internal set; }
        public string ParentTypeName { get; internal set; }
    }
}
