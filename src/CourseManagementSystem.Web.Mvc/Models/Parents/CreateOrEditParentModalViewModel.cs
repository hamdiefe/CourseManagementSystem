using CourseManagementSystem.Services.Parents.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Parents
{
    public class CreateOrEditParentModalViewModel
    {
        public CreateOrEditParentDto Parent { get; set; }

        public bool IsEditMode => Parent.Id.HasValue;

        public string JobName { get; set; }

        public string ParentTypeName { get; set; }

        public List<ParentJobLookupTableDto> ParentJobList { get; set; }
        public List<ParentParentTypeLookupTableDto> ParentParentTypeList { get; set; }
        public List<ParentCountryLookupTableDto> ParentCountryList { get; set; }
    }
}
