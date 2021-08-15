using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseManagementSystem.Services.Documents.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Documents
{
    public interface IDocumentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetDocumentForViewDto>> GetAll(GetAllDocumentsInput input);

        Task<GetDocumentForViewDto> GetDocumentForView(int id);

        Task<GetDocumentForEditOutput> GetDocumentForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditDocumentDto input);

        Task Delete(EntityDto input);

        Task DeleteAll(List<int> input);

        Task<List<DocumentStudentLookupTableDto>> GetStudentsForTableDropdown();
    }
}
