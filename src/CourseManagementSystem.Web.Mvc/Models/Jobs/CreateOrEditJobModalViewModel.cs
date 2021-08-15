using CourseManagementSystem.Services.Jobs.Dtos;

namespace CourseManagementSystem.Web.Models.Jobs
{
    public class CreateOrEditJobModalViewModel
    {
        public CreateOrEditJobDto Job { get; set; }

        public bool IsEditMode => Job.Id.HasValue;
    }
}
