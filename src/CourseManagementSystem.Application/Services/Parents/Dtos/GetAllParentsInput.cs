using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Services.Parents.Dtos
{
    public class GetAllParentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public byte? GenderFilter { get; set; }

        public string ParentTypeNameFilter { get; set; }

        public string JobNameFilter { get; set; }
    }
}
