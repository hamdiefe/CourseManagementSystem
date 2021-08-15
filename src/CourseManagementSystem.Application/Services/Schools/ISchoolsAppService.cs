using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseManagementSystem.Services.Schools.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Schools
{
    public interface ISchoolsAppService : IApplicationService
    {
        Task<PagedResultDto<GetSchoolForViewDto>> GetAll(GetAllSchoolsInput input);

        Task<GetSchoolForViewDto> GetSchoolForView(int id);

        Task<GetSchoolForEditOutput> GetSchoolForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditSchoolDto input);

        Task Delete(EntityDto input);

        Task DeleteAll(List<int> input);
    }
}
