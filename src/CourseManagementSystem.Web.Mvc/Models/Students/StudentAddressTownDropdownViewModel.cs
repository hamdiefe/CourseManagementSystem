using CourseManagementSystem.Services.Students.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Students
{
    public class StudentAddressTownDropdownViewModel
    {
        public ICollection<StudentTownLookupTableDto> StudentTownList { get; set; }
        public int? SelectedValue { get; set; }

        public StudentAddressTownDropdownViewModel()
        {
            StudentTownList = new HashSet<StudentTownLookupTableDto>();
        }
    }
}
