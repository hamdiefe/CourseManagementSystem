using CourseManagementSystem.Services.Teachers.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Teachers
{
    public class AddTeacherPhoneFieldViewModel
    {
        public List<TeacherCountryLookupTableDto> TeacherCountryList { get; internal set; }
    }
}
