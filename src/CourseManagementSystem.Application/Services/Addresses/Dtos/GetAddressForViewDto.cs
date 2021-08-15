namespace CourseManagementSystem.Services.Addresses.Dtos
{
    public class GetAddressForViewDto
    {
        public AddressDto Address { get; set; }

        public string CountryName { get;  set; }

        public string CityName { get; set; }

        public string TownName { get; set; }
    }
}
