using CourseManagementSystem.Services.Addresses.Dtos;
using CourseManagementSystem.Services.Phones.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Parents.Dtos
{
    public class GetParentForViewDto
    {
        public ParentDto Parent { get; set; }

        public string JobName { get; set; }

        public string ParentTypeName { get; set; }

        public List<GetPhoneForViewDto> Phones { get; set; }

        public List<GetAddressForViewDto> Addresses { get; set; }
    }
}
