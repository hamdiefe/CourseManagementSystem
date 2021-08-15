using CourseManagementSystem.Services.Documents.Dtos;
using System.Collections.Generic;

namespace CourseManagementSystem.Web.Models.Documents
{
    public class CreateOrEditDocumentModalViewModel
    {
        public CreateOrEditDocumentDto Document { get; set; }

        public List<DocumentStudentLookupTableDto> DocumentStudentList { get; set; }

        public bool IsEditMode => Document.Id.HasValue;
    }
}
