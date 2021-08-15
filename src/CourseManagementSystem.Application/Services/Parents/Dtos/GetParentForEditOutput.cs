namespace CourseManagementSystem.Services.Parents.Dtos
{
    public class GetParentForEditOutput
    {
        public CreateOrEditParentDto Parent { get; set; }

        public string JobName { get; set; }

        public string ParentTypeName { get; set; }
    }
}
