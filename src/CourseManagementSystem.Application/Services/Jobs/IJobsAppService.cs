using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseManagementSystem.Services.Jobs.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Jobs
{
    public interface IJobsAppService : IApplicationService
    {
        Task<PagedResultDto<GetJobForViewDto>> GetAll(GetAllJobsInput input);

        Task<GetJobForViewDto> GetJobForView(int id);

        Task<GetJobForEditOutput> GetJobForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditJobDto input);

        Task Delete(EntityDto input);

        Task DeleteAll(List<int> input);
    }
}
