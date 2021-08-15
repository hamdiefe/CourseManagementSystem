using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.ParentTypes;

namespace CourseManagementSystem.Services.ParentTypes.Dtos
{
    public class ParentTypeDto : EntityDto
    {
        public string Name { get; set; }

        public ParentDegree Degree { get; set; }
    }
}
