using CourseManagementSystem.Services.Addresses.Dtos;
using CourseManagementSystem.Services.Phones.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Teachers.Dtos
{
    public class GetTeacherForViewDto
    {
        public TeacherDto Teacher { get; set; }

        public string GraduationSchoolName { get; set; }

        public List<GetPhoneForViewDto> Phones { get; set; }

        public List<GetAddressForViewDto> Addresses { get; set; }
    }
}
