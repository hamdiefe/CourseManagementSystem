using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Services.ParentTypes.Dtos
{
    public class GetAllParentTypesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
