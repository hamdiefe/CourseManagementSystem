using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Events;
using System.Collections.Generic;

namespace CourseManagementSystem.Services.Events.Dtos
{
    public class CalendarEventDataOutput
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public string ClassName { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int TotalHours { get; set; }

        public EventType Type { get; set; }
        public List<CalendarEventStudentDto> Students { get; set; }
        public List<CalendarEventTeacherDto> Teachers { get; set; }
    }


    public class CalendarEventStudentDto
    {
        public string Value { get; set; }
        public string Src { get; set; }
        public Gender Gender { get; set; }
        public int Id { get; set; }
    }

    public class CalendarEventTeacherDto
    {
        public string Value { get; set; }
        public string Src { get; set; }
        public Gender Gender { get; internal set; }
        public int Id { get; set; }
    }



}
