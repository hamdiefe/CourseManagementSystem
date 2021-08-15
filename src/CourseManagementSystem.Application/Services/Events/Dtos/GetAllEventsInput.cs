using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Services.Events.Dtos
{
    public class GetAllEventsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
