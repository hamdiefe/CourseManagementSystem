using CourseManagementSystem.Services.Events.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Events
{
    public class CreateOrEditEventModalViewModel
    {
        public CreateOrEditEventDto Event { get; set; }

        public bool IsEditMode => Event.Id.HasValue;

        public List<EventStudentLookupTableDto> EventStudentList { get; set; }
        public List<EventTeacherLookupTableDto> EventTeacherList { get; set; }
    }
}
