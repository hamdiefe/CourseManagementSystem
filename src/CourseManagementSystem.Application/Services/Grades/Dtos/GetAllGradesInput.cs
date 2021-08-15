using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Services.Grades.Dtos
{
    public class GetAllGradesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
