using CourseManagementSystem.Services.Parents.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Parents
{
    public class ParentAddressCityDropdownViewModel
    {
        public ICollection<ParentCityLookupTableDto> ParentCityList { get; set; }
        public int? SelectedValue { get; set; }

        public ParentAddressCityDropdownViewModel()
        {
            ParentCityList = new HashSet<ParentCityLookupTableDto>();
        }
    }
}
