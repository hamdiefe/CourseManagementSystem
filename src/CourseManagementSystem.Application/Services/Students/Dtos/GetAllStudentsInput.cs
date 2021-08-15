using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Services.Students.Dtos
{
    public class GetAllStudentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public byte? GenderFilter { get; set; }

        public string GradeNameFilter { get; set; }

        public string SchoolNameFilter { get; set; }
    }
}
