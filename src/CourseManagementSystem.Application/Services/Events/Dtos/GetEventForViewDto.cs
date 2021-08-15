namespace CourseManagementSystem.Services.Events.Dtos
{
    public class GetEventForViewDto
    {
        public EventDto Event { get; set; }

        public string StudentName { get; set; }

        public string TeacherName { get; set; }
    }
}
