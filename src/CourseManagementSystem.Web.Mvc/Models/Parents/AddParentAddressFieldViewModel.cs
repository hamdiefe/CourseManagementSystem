using CourseManagementSystem.Services.Parents.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Parents
{
    public class AddParentAddressFieldViewModel
    {
        public List<ParentCountryLookupTableDto> ParentCountryList { get; internal set; }
    }
}
