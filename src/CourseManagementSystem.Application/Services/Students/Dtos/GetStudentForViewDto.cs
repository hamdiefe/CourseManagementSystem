using CourseManagementSystem.Services.Addresses.Dtos;
using CourseManagementSystem.Services.Phones.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Students.Dtos
{
    public class GetStudentForViewDto
    {
        public StudentDto Student { get; set; }
        public string SchoolName { get; set; }
        public string GradeName { get; set; }
        public List<GetPhoneForViewDto> Phones { get; set; }
        public List<GetAddressForViewDto> Addresses { get; set; }
    }
}
