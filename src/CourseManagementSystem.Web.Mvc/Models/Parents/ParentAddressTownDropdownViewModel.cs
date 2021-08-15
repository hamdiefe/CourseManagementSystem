using CourseManagementSystem.Services.Parents.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Parents
{
    public class ParentAddressTownDropdownViewModel
    {
        public ICollection<ParentTownLookupTableDto> ParentTownList { get; set; }
        public int? SelectedValue { get; set; }

        public ParentAddressTownDropdownViewModel()
        {
            ParentTownList = new HashSet<ParentTownLookupTableDto>();
        }
    }
}
