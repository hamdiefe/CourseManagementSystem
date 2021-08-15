using CourseManagementSystem.Services.Students.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Students
{
    public class AddStudentPhoneFieldViewModel
    {
        public List<StudentCountryLookupTableDto> StudentCountryList { get; internal set; }
    }
}
