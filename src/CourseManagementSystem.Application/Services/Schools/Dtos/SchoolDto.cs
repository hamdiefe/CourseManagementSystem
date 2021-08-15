using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Schools;

namespace CourseManagementSystem.Services.Schools.Dtos
{
    public class SchoolDto : EntityDto
    {
        public string Name { get; set; }

        public SchoolType Type { get; set; }
    }
}
