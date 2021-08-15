using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Addresses;

namespace CourseManagementSystem.Services.Addresses.Dtos
{
    public class AddressDto : EntityDto
    {    
        public string Detail { get; set; }

        public string District { get; set; }

        public int? TownId { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public AddressType Type { get; set; }

        public int? StudentId { get; set; }
    }
}
