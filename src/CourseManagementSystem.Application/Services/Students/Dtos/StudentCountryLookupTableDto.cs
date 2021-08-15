namespace CourseManagementSystem.Services.Students.Dtos
{
    public class StudentCountryLookupTableDto
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string PhoneCode { get; set; }

        public string DualCode { get; internal set; }
    }
}
