using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Services.Jobs.Dtos
{
    public class GetAllJobsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
