using CourseManagementSystem.Services.Teachers.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Teachers
{
    public class TeacherAddressCityDropdownViewModel
    {
        public ICollection<TeacherCityLookupTableDto> TeacherCityList { get; set; }

        public int? SelectedValue { get; set; }

        public TeacherAddressCityDropdownViewModel()
        {
            TeacherCityList = new HashSet<TeacherCityLookupTableDto>();
        }
    }
}
