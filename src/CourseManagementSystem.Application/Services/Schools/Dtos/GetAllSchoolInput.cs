using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Services.Schools.Dtos
{
    public class GetAllSchoolsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public byte? TypeFilter { get; set; }
    }
}
