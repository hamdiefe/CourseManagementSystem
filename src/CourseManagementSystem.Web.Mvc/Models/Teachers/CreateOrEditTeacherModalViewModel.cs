using CourseManagementSystem.Services.Teachers.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Teachers
{
    public class CreateOrEditTeacherModalViewModel
    {
        public CreateOrEditTeacherDto Teacher { get; set; }

        public bool IsEditMode => Teacher.Id.HasValue;

        public string GradeName { get; set; }

        public string GraduationSchoolName { get; set; }

        public List<TeacherGraduationSchoolLookupTableDto> TeacherGraduationSchoolList { get; set; }
        public List<TeacherCountryLookupTableDto> TeacherCountryList { get;  set; }
    }
}
