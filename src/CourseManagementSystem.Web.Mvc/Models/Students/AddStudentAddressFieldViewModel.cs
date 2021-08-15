using CourseManagementSystem.Services.Students.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Students
{
    public class AddStudentAddressFieldViewModel
    {
        public List<StudentCountryLookupTableDto> StudentCountryList { get; internal set; }
    }
}
