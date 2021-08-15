using CourseManagementSystem.Services.Students.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Students
{
    public class CreateOrEditStudentModalViewModel
    {
        public CreateOrEditStudentDto Student { get; set; }

        public bool IsEditMode => Student.Id.HasValue;

        public string GradeName { get; set; }

        public string SchoolName { get; set; }

        public List<StudentGradeLookupTableDto> StudentGradeList { get; set; }
        public List<StudentSchoolLookupTableDto> StudentSchoolList { get; set; }
        public List<StudentCountryLookupTableDto> StudentCountryList { get;  set; }
        public List<StudentParentLookupTableDto> StudentParentList { get;  set; }
    }
}
