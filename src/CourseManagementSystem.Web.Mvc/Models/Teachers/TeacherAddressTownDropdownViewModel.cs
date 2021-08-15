using CourseManagementSystem.Services.Teachers.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Teachers
{
    public class TeacherAddressTownDropdownViewModel
    {
        public ICollection<TeacherTownLookupTableDto> TeacherTownList { get; set; }

        public int? SelectedValue { get; set; }

        public TeacherAddressTownDropdownViewModel()
        {
            TeacherTownList = new HashSet<TeacherTownLookupTableDto>();
        }
    }
}
