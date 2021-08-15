using Abp.Application.Services.Dto;

namespace CourseManagementSystem.Services.Jobs.Dtos
{
    public class JobDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
