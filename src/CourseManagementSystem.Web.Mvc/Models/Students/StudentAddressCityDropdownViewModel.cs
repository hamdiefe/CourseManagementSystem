using CourseManagementSystem.Services.Students.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Students
{
    public class StudentAddressCityDropdownViewModel
    {
        public ICollection<StudentCityLookupTableDto> StudentCityList { get; set; }
        public int? SelectedValue { get; set; }

        public StudentAddressCityDropdownViewModel()
        {
            StudentCityList = new HashSet<StudentCityLookupTableDto>();
        }
    }
}
