using CourseManagementSystem.Services.ParentTypes.Dtos;

namespace CourseManagementSystem.Web.Models.ParentTypes
{
    public class CreateOrEditParentTypeModalViewModel
    {
        public CreateOrEditParentTypeDto ParentType { get; set; }

        public bool IsEditMode => ParentType.Id.HasValue;
    }
}
