using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseManagementSystem.Services.Events.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Events
{
    public interface IEventsAppService : IApplicationService
    {
        Task<PagedResultDto<GetEventForViewDto>> GetAll(GetAllEventsInput input);

        Task<GetEventForViewDto> GetEventForView(int id);

        Task<GetEventForEditOutput> GetEventForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditEventDto input);

        Task Delete(EntityDto input);

        Task DeleteAll(List<int> input);

        Task<List<EventStudentLookupTableDto>> GetStudentsForTableDropdown();

        Task<List<EventTeacherLookupTableDto>> GetTeachersForTableDropdown();
    }
}
