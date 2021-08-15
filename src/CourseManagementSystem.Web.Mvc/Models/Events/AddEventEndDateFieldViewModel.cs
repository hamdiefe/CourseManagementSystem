using System;

namespace CourseManagementSystem.Web.Models.Events
{
    public class AddEventEndDateFieldViewModel
    {
        public DateTime MinDate { get; set; }

        public DateTime? SelectedEndDate { get; set; }
    }
}
