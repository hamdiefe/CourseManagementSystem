using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Services.Documents.Dtos
{
    public class GetAllDocumentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
