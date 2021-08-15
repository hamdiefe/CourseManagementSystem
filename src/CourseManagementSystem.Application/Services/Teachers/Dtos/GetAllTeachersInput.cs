using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Services.Teachers.Dtos
{
    public class GetAllTeachersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public byte? GenderFilter { get; set; }

        public string SchoolNameFilter { get; set; }
    }
}
