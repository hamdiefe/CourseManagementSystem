namespace CourseManagementSystem.Services.Teachers.Dtos
{
    public class TeacherCountryLookupTableDto
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string PhoneCode { get; set; }

        public string DualCode { get; internal set; }
    }
}
