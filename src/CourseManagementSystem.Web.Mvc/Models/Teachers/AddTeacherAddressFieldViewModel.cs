using CourseManagementSystem.Services.Teachers.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Teachers
{
    public class AddTeacherAddressFieldViewModel
    {
        public List<TeacherCountryLookupTableDto> TeacherCountryList { get; internal set; }
    }
}
