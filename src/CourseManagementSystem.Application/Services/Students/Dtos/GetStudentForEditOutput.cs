namespace CourseManagementSystem.Services.Students.Dtos
{
    public class GetStudentForEditOutput
    {
        public CreateOrEditStudentDto Student { get; set; }

        public string GradeName { get; set; }

        public string SchoolName { get; set; }
    }
}
