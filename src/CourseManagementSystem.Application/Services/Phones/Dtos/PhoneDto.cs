using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Phones;

namespace CourseManagementSystem.Services.Phones.Dtos
{
    public class PhoneDto : EntityDto
    {
        public string Code { get; set; }

        public string Number { get; set; }

        public PhoneType Type { get; set; }

        public int? StudentId { get; set; }
    }
}
